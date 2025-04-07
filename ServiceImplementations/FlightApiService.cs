using ServiceContracts;
using Shared.Models;
using System.Net.Http.Json;

namespace ServiceImplementations
{
    /// <summary>
    /// Service der kommunikerer med TravelFusionLeanApi for at hente flydata.
    /// </summary>
    public class FlightApiService(HttpClient httpClient) : IFlightApiService
    {
        private readonly HttpClient _httpClient = httpClient;

        /// <inheritdoc />
        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Flight>>("https://localhost:7274/api/flight");
            return response ?? new List<Flight>();
        }

        /// <inheritdoc />
        public async Task<Flight?> GetFlightByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Flight>($"https://localhost:7274/api/flight/{id}");
        }
    }
}