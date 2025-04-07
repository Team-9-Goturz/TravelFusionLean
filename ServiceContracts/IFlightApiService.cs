using Shared.Models;

namespace ServiceContracts
{
    /// <summary>
    /// Interface til kommunikation med eksternt Flight API.
    /// </summary>
    public interface IFlightApiService
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight?> GetFlightByIdAsync(int id);
    }
}