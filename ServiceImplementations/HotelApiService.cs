using ServiceContracts;
using Dtos;
using System.Net.Http.Json;

namespace ServiceImplementations;

/// <summary>
/// Service der kommunikerer med TravelFusionLeanApi for at hente hoteldata.
/// </summary>
public class HotelApiService(HttpClient httpClient) : IHotelApiService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<HotelDto>>("https://localhost:7274/api/hotel");
        return response ?? new List<HotelDto>();
    }

    public async Task<HotelDto?> GetHotelByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<HotelDto>($"https://localhost:7274/api/hotel/{id}");
    }
}