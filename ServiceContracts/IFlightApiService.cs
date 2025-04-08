using Shared.Dtos;

namespace ServiceContracts
{
    /// <summary>
    /// Interface til kommunikation med eksternt Flight API.
    /// </summary>
    public interface IFlightApiService
    {
        Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
        Task<FlightDto?> GetFlightByIdAsync(int id);
    }
}