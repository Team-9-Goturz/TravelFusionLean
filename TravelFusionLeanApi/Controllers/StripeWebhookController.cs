using Microsoft.AspNetCore.Mvc;
using Stripe;
using Session = Stripe.Checkout.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using System.IO;
using System.Threading.Tasks;



namespace TravelFusionLeanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripeWebhookController> _logger;

        public StripeWebhookController(IConfiguration configuration, ILogger<StripeWebhookController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            _logger.LogInformation("Modtog Stripe webhook: {Json}", json);

            var stripeSignature = Request.Headers["Stripe-Signature"];
            var webhookSecret = _configuration["Stripe:WebhookSecret"];

            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, webhookSecret);
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe signatur validering fejlede.");
                return BadRequest($"Invalid Stripe signature: {ex.Message}");
            }

            // Valider event type
            if (stripeEvent.Type == "checkout.session.completed")
            {
                try
                {
                    var session = stripeEvent.Data.Object as Session;
                    if (session == null)
                    {
                        _logger.LogWarning("Stripe webhook modtog en CheckoutSessionCompleted, men session var null.");
                        return BadRequest("Invalid session data");
                    }

                    _logger.LogInformation("Stripe session modtaget: {SessionId}", session.Id);

                    // TODO: Find og opdater betaling i din database vha. session.Id
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fejl ved behandling af CheckoutSessionCompleted.");
                    return StatusCode(500, "Intern fejl i webhook-handler");
                }
            }
            else
            {
                _logger.LogWarning("Webhook-type ikke håndteret: {EventType}", stripeEvent.Type);
            }

            return Ok();
        }
    }
}