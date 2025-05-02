using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceImplementations;
using Shared.Dtos;
using Shared.Models;
using TravelFusionLeanApi.Models;

namespace TravelFusionLeanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly StripeService _stripeService;

        public StripeController(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("create-checkout-session")]
        public async Task<ActionResult<string>> CreateCheckoutSession([FromBody] StripeCheckoutDTO dto)
        {
            try
            {
                var session = await _stripeService.CreateCheckoutSessionAsync(dto);
                var response = new StripeSessionResponseDTO
                {
                    Url = session.Url,
                    StripeSessionId = session.Id,
                };

                return Ok(response); // URL til stripe checkout siden
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Stripe fejl: {ex.Message}");
            }
        }
    }
}

