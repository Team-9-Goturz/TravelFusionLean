using ServiceImplementations.Dtos;
using Shared.Models;

namespace ServiceImplementations.Mappers;

/// <summary>
/// Mapper HotelData (fra MockHotelsAPI) til Hotel model (EF Core).
/// </summary>
public static class HotelMapper
{
    public static Hotel MapToHotel(HotelData dto)
    {
        return new Hotel
        {
            Name = dto.Name,
            Description = $"{dto.StarRating}-star hotel – Rating {dto.Rating}/10",
            Address = $"{dto.CityCode}, {dto.CountryCode}",
            Latitude = (decimal)(dto.GeoCode?.Latitude ?? 0),
            Longitude = (decimal)(dto.GeoCode?.Longitude ?? 0),
            //  kan tilføje dto.StarRating til at udvide modellen
        };
    }

    public static HotelStay MapToHotelStay(HotelData dto, Currency currency, DateTime? checkIn = null, DateTime? checkOut = null, int travellers = 2)
    {
        var checkInDate = checkIn ?? DateTime.Today.AddDays(7);
        var checkOutDate = checkOut ?? DateTime.Today.AddDays(10);

        return new HotelStay
        {
            Hotel = MapToHotel(dto),
            Price = dto.Price?.Total ?? 0,
            Currency = currency,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            DaysOfStay = (checkOutDate - checkInDate).Days,
            NoOfTravellers = travellers
        };
    }
}
