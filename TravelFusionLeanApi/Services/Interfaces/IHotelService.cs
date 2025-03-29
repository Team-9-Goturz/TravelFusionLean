namespace TravelFusionLeanApi.Services
{
    /// <summary>
    /// Interface for HotelService, definerer funktioner til at hente hoteldata fra mock-API'et.
    /// </summary>
    public interface IHotelService
    {
        /// <summary>
        /// Henter en liste af alle tilgængelige hoteller fra mock-API'et.
        /// </summary>
        /// <returns>En liste af HotelData objekter.</returns>
        Task<IEnumerable<HotelData>> GetHotelsAsync();

        /// <summary>
        /// Henter ét specifikt hotel baseret på hotelId.
        /// </summary>
        /// <param name="hotelId">Hotellets identifikator (f.eks. GVJH7D2A)</param>
        /// <returns>Et HotelData objekt hvis fundet, ellers null.</returns>
        Task<HotelData?> GetHotelByIdAsync(string hotelId);
    }
}
