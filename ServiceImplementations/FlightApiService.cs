using ServiceContracts;
using Shared.Models;
using System.Net.Http.Json;
using ServiceImplementations.Dtos;

namespace ServiceImplementations;

/// <summary>
/// Service der kommunikerer med TravelFusionLeanApi for at hente flydata som DTO’er.
/// </summary>
public class FlightApiService(HttpClient httpClient) : IFlightApiService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<FlightDto>>("https://localhost:7274");
        var flights = new List<Flight>();
        if (response == null) return flights;
        foreach (var flight in response)
        {
            flights.Add(Mappers.FlightMapper.MapToModel(flight.Data.FirstOrDefault()));
        }
        return flights;
    }

    public async Task<Flight?> GetFlightByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Flight>($"https://localhost:7274/api/Flights/{id}");
    }
}
