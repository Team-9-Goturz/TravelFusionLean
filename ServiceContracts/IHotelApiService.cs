using Shared.Dtos;

namespace ServiceContracts;

/// <summary>
/// Interface for API-service til at hente hoteldata fra TravelFusionLeanApi.
/// </summary>
public interface IHotelApiService
{
    /// <summary>
    /// Henter alle hoteller fra API'et.
    /// </summary>
    Task<IEnumerable<HotelDto>> GetAllHotelsAsync();

    /// <summary>
    /// Henter et specifikt hotel baseret på ID.
    /// </summary>
    Task<HotelDto?> GetHotelByIdAsync(int id);
}