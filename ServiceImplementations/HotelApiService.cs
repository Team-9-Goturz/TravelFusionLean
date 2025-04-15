using ServiceContracts;
using Shared.Models;
using System.Net.Http.Json;
using ServiceImplementations.Dtos;
using ServiceImplementations.Mappers;


namespace ServiceImplementations;

/// <summary>
/// Service der henter hoteldata fra TravelFusionLeanApi via HTTP.
/// </summary>
public class HotelApiService(HttpClient httpClient) : IHotelApiService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<HotelStay>> GetAllHotelStaysAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<HotelData>>("https://localhost:7274/api/hotels");

        var hotelStays = new List<HotelStay>();
        if (response == null) return hotelStays;

        foreach (var hotelData in response)
        {
            var currency = new Currency
            {
                Name = hotelData.Price?.Currency ?? "DKK"
            };

            hotelStays.Add(HotelMapper.MapToHotelStay(hotelData, currency));
        }

        return hotelStays;
    }

    public async Task<HotelStay?> GetHotelStayByIdAsync(int id)
    {
        var hotelData = await _httpClient.GetFromJsonAsync<HotelData>($"https://localhost:7274/api/hotels/{id}");
        if (hotelData == null) return null;

        var currency = new Currency
        {
            Name = hotelData.Price?.Currency ?? "DKK"
        };

        return HotelMapper.MapToHotelStay(hotelData, currency);
    }
}