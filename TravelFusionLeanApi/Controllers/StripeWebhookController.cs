using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.Models;
using Stripe;
using Session = Stripe.Checkout.Session;

namespace TravelFusionLeanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripeWebhookController> _logger;
        private readonly IPaymentService _paymentService;
        private readonly IBookingService _bookingService;

        public StripeWebhookController(IConfiguration configuration, ILogger<StripeWebhookController> logger, IPaymentService paymentService, IBookingService bookingService)
        {
            _configuration = configuration;
            _logger = logger;
            _paymentService = paymentService;
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            _logger.LogInformation("Webhook endpoint blev kaldt!");
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

                    _logger.LogInformation("Stripe session modtaget: {SessionId}", session.Id);
                    Payment payment = await _paymentService.MarkPaymentAsSucceededAsync(session.Id, session.PaymentIntentId);
                    await _bookingService.MarkBookingAsPaidAsync(payment.BookingId);
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