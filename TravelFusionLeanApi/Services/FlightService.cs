using TravelFusionLeanApi.Interfaces;
using TravelFusionLeanApi.Models;

namespace TravelFusionLeanApi.Services
{
    /// <summary>
    /// Kalder MockFlightsAPI og returnerer flydata.
    /// </summary>
    public class FlightService : IFlightService
    {
        private readonly HttpClient _httpClient;

        public FlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FlightDto>> GetFlightsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<FlightDto>>("api/flights") ?? new();
        }

        public async Task<FlightDto?> GetFlightByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<FlightDto>($"api/flights/{id}");
        }
    }

}
