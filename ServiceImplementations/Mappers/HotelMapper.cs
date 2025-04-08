using ServiceImplementations.Dtos;
using Shared.Models;

namespace ServiceImplementations.Mappers;

/// <summary>
/// Mapper HotelData (fra MockHotelsAPI) til Hotel model (EF Core).
/// </summary>
public static class HotelMapper
{
    public static Hotel MapToModel(HotelData dto)
    {
        return new Hotel
        {
            Name = dto.Name,
            Description = $"{dto.StarRating}-star hotel in {dto.CityCode}. User rating: {dto.Rating}",
            Address = $"{dto.CityCode} Centrum",
            Latitude = (decimal)(dto.GeoCode?.Latitude ?? 0),
            Longitude = (decimal)(dto.GeoCode?.Longitude ?? 0)
        };
    }
}