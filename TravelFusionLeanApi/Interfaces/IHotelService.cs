using TravelFusionLeanApi.Models;

namespace TravelFusionLeanApi.Interfaces
{
    /// <summary>
    /// Interface til at hente hoteldata fra MockHotelsAPI.
    /// </summary>
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetHotelsAsync();
        Task<HotelDto?> GetHotelByIdAsync(string id);
    }
}
