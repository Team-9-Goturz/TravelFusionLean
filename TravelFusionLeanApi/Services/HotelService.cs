using TravelFusionLeanApi.Interfaces;
using TravelFusionLeanApi.Models;

namespace TravelFusionLeanApi.Services
{
    /// <summary>
    /// Kalder MockHotelsAPI og returnerer hoteldata.
    /// </summary>
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<HotelDto>> GetHotelsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<HotelResponse>("api/hotels");
            return response?.Data ?? new();
        }

        public async Task<HotelDto?> GetHotelByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<HotelDto>($"api/hotels/{id}");
        }
    }

}
