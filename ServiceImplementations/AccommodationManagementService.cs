using ServiceContracts;
using Shared.Models;
using System.Net.Http.Json;
using ServiceImplementations.Dtos;
using ServiceImplementations.Mappers;
using Microsoft.EntityFrameworkCore;


namespace ServiceImplementations;

/// <summary>
/// Service der henter hoteldata fra TravelFusionLeanApi via HTTP.
/// </summary>
public class AccommodationManagementService(HttpClient httpClient, ICountryReadService countryReadService) : IHotelApiService
{
    private readonly ICountryReadService _countryReadService = countryReadService;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<HotelStay>> GetAllHotelStaysAsync()
    {
        var hotelStays = new List<HotelStay>();

        //Map JSON til Hoteldata
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<HotelData>>("https://localhost:7274/api/hotels");
        if (response == null) return hotelStays;

        // Find unikke landekoder
        var countryCodes = response.Select(h => h.CountryCode).Distinct().ToList();
        var countries = await _countryReadService.GetCountriesByCodeAsync(countryCodes); 

        foreach (HotelData hotelData in response)
        {
            countries.TryGetValue(hotelData.CountryCode, out Country country);
            hotelStays.Add(HotelMapper.MapToHotelStay(hotelData, country));
        }

        return hotelStays;
    }

    public async Task<HotelStay?> GetHotelStayByIdAsync(int id)
    {
        var hotelData = await _httpClient.GetFromJsonAsync<HotelData>($"https://localhost:7274/api/hotels/{id}");
        if (hotelData == null) return null;

        Country country = await _countryReadService.GetByCountryCodeAsync(hotelData.CountryCode);
   

        return HotelMapper.MapToHotelStay(hotelData,country);
    }
}