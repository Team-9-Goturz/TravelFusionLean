using Shared.Models;

namespace ServiceContracts;

/// <summary>
/// Interface for API-service til at hente hoteldata som ophold fra TravelFusionLeanApi.
/// </summary>
public interface IHotelApiService
{
    /// <summary>
    /// Henter alle hotelophold fra API'et. inkl hotel, pris og valuta
    /// </summary>
    Task<IEnumerable<HotelStay>> GetAllHotelStaysAsync();

    /// <summary>
    /// Henter et specifikt hotel baseret på ID. (mangler i api'et)
    /// </summary>
    Task<HotelStay?> GetHotelStayByIdAsync(int id);
}