using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelFusionLeanApi.Services;

namespace TravelFusionLeanApi.Controllers
{
    /// <summary>
    /// Controller til at hente hoteldata fra MockHotelsAPI.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _hotelService.GetHotelsAsync();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }
    }
}
