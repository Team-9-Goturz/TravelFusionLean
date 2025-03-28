using TravelFusionLeanApi.Models;

namespace TravelFusionLeanApi.Interfaces
{
    /// <summary>
    /// Interface til at hente flydata fra MockFlightsAPI.
    /// </summary>
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetFlightsAsync();
        Task<FlightDto?> GetFlightByIdAsync(string id);
    }
}