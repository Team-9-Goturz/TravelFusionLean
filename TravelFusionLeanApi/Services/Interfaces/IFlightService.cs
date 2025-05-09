namespace TravelFusionLeanApi.Services
{
    /// <summary>
    /// Interface for FlightModelService, definerer funktioner til at hente flydata fra eksternt API.
    /// </summary>
    public interface IFlightService
    {
        /// <summary>
        /// Henter en liste af alle tilgængelige fly fra mock-API'et.
        /// </summary>
        /// <returns>En liste af FlightData objekter.</returns>
        Task<IEnumerable<FlightData>> GetFlightsAsync();

        /// <summary>
        /// Henter ét specifikt fly baseret på flightId.
        /// </summary>
        /// <param name="flightId">Flyets identifikator (f.eks. FL113)</param>
        /// <returns>Et FlightData objekt hvis fundet, ellers null.</returns>
        Task<FlightData?> GetFlightByIdAsync(string flightId);
    }
}
