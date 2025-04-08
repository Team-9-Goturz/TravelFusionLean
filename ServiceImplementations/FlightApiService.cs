using ServiceContracts;
using Dtos;
using Shared.Models;
using System.Net.Http.Json;

namespace ServiceImplementations;

/// <summary>
/// Service der kommunikerer med TravelFusionLeanApi for at hente flydata som DTO’er.
/// </summary>
public class FlightApiService(HttpClient httpClient) : IFlightApiService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<FlightDto>>("https://localhost:7274/api/flight");
        return response ?? new List<FlightDto>();
    }

    public async Task<FlightDto?> GetFlightByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<FlightDto>($"https://localhost:7274/api/flight/{id}");
    }
}
