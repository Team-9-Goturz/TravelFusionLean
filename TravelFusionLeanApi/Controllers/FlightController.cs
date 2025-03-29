using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelFusionLeanApi.Services;


namespace TravelFusionLeanApi.Controllers
{
    /// <summary>
    /// Controller til at hente flydata fra MockFlightsAPI.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _flightService.GetFlightsAsync();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null) return NotFound();
            return Ok(flight);
        }
    }
}
