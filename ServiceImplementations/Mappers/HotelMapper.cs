using Shared.Dtos;
using Shared.Models;

namespace ServiceImplementations.Mappers;

/// <summary>
/// Mapper klasse til at konvertere mellem HotelDto og Hotel EF-model.
/// </summary>
public static class HotelMapper
{
    /// <summary>
    /// Konverterer en HotelDto til en EF Hotel-model.
    /// Bruges når API-data skal gemmes i databasen.
    /// </summary>
    public static Hotel ToEntity(HotelDto dto)
    {
        return new Hotel
        {
            Name = dto.Name,
            Address = $"{dto.CityCode}, {dto.CountryCode}", // Simpel sammensætning
            Latitude = dto.GeoCode?.Latitude ?? 0,
            Longitude = dto.GeoCode?.Longitude ?? 0,
            Description = $"Stars: {dto.StarRating}, Rating: {dto.Rating}" // Valgfri opsummering
        };
    }

    /// <summary>
    /// Konverterer en EF Hotel til en HotelDto.
    /// Bruges når EF-data skal sendes til UI eller API.
    /// </summary>
    public static HotelDto ToDto(Hotel hotel)
    {
        return new HotelDto
        {
            Name = hotel.Name,
            CityCode = hotel.Address?.Split(',')[0].Trim() ?? "N/A",
            CountryCode = hotel.Address?.Split(',').Length > 1 ? hotel.Address.Split(',')[1].Trim() : "N/A",
            GeoCode = new GeoCodeInfo
            {
                Latitude = hotel.Latitude,
                Longitude = hotel.Longitude
            },
            StarRating = 3, // Dummy – dette kan ikke mappes direkte fra EF uden ekstra kolonne
            Rating = 4.2,   // Dummy
            Price = new PriceInfo
            {
                Total = 999, // Dummy – afhænger af HotelStay
                Currency = "EUR"
            },
            Available = true,
            Images = new List<string> { "/images/hotel.jpg" }
        };
    }
}