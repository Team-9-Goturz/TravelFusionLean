using Shared.Models;

namespace ServiceContracts
{
    /// <summary>
    /// Interface til kommunikation med eksternt Hotel API.
    /// </summary>
    public interface IHotelApiService
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        Task<Hotel?> GetHotelByIdAsync(int id);
    }
}