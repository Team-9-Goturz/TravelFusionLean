using ServiceContracts;
using Shared.Models;
using System.Net.Http.Json;

namespace ServiceImplementations
{
    /// <summary>
    /// Service der kommunikerer med TravelFusionLeanApi for at hente hoteldata.
    /// </summary>
    public class HotelApiService(HttpClient httpClient) : IHotelApiService
    {
        private readonly HttpClient _httpClient = httpClient;

        /// <inheritdoc />
        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Hotel>>("https://localhost:7274/api/hotel");
            return response ?? new List<Hotel>();
        }

        /// <inheritdoc />
        public async Task<Hotel?> GetHotelByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Hotel>($"https://localhost:7274/api/hotel/{id}");
        }
    }
}