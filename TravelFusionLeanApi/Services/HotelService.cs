using TravelFusionLeanApi.Services;

/// <summary>
/// Service til håndtering af kald til MockHotelsAPI.
/// </summary>
public class HotelService : IHotelService
{
    private readonly HttpClient _httpClient;

    public HotelService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Henter alle hoteller fra mock-API'et.
    /// </summary>
    public async Task<IEnumerable<HotelData>> GetHotelsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<HotelResponse>("api/hotels");
        return response?.Data ?? new List<HotelData>();
    }

    /// <summary>
    /// Henter ét specifikt hotel baseret på hotelId.
    /// </summary>
    public async Task<HotelData?> GetHotelByIdAsync(string hotelId)
    {
        var hotels = await GetHotelsAsync();
        return hotels.FirstOrDefault(h => h.HotelId == hotelId);
    }
}
