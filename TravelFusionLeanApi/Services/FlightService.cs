using System.Net.Http.Json;

namespace TravelFusionLeanApi.Services
{
    /// <summary>
    /// Service til håndtering af kald til MockFlightsAPI.
    /// </summary>
    public class FlightService : IFlightService
    {
        private readonly HttpClient _httpClient;

        public FlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Henter alle fly fra mock-API'et.
        /// </summary>
        public async Task<IEnumerable<FlightData>> GetFlightsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<FlightResponse>("api/flights");
            return response?.Data ?? new List<FlightData>();
        }

        /// <summary>
        /// Henter et specifikt fly baseret på flight ID.
        /// </summary>
        public async Task<FlightData?> GetFlightByIdAsync(string flightId)
        {
            var allFlights = await GetFlightsAsync();
            return allFlights.FirstOrDefault(f => f.Flight.IATA == flightId);
        }
    }
}