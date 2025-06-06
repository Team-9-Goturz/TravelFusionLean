﻿using ServiceContracts;
using Shared.Models;
using System.Net.Http.Json;
using ServiceImplementations.Dtos;

namespace ServiceImplementations;

/// <summary>
/// Service der kommunikerer med TravelFusionLeanApi for at hente flydata som DTO’er.
/// </summary>
public class FlightRouteAPIService(HttpClient httpClient) : IFlightApiService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<FlightData>>("https://localhost:7274/api/flights/");
        var flights = new List<Flight>();
        if (response == null) return flights;
        foreach (var flightData in response)
        {
            flights.Add(Mappers.FlightMapper.MapToModel(flightData));
        }
        return flights;
    }

    public async Task<Flight?> GetFlightByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Flight>($"https://localhost:7274/api/flights/{id}");
    }
}
