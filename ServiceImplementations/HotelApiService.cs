using ServiceContracts;
using Shared.Models;
using System.Net.Http.Json;
using ServiceImplementations.Dtos;
using ServiceImplementations.Mappers;

namespace ServiceImplementations;

/// <summary>
/// Service der kommunikerer med TravelFusionLeanApi for at hente hoteldata.
/// </summary>
public class HotelApiService(HttpClient httpClient) : IHotelApiService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<HotelDto>>("https://localhost:7274/api/hotel");
        var hotels = new List<Hotel>();
        if (response == null) return hotels;
        foreach (var hotelDto in response.ToList().FirstOrDefault().Data)
        {
            hotels.Add(HotelMapper.MapToModel(hotelDto));
        }

        return hotels;
    }

    public async Task<Hotel?> GetHotelByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Hotel>($"https://localhost:7274/api/hotel/{id}");
    }
}