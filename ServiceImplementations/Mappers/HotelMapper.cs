using ServiceImplementations.Dtos;
using Shared.Models;
using static Shared.Models.Price;

namespace ServiceImplementations.Mappers;

/// <summary>
/// Mapper HotelData (fra MockHotelsAPI) til Hotel model (EF Core).
/// </summary>
public static class HotelMapper
{
    public static Hotel MapToHotel(HotelData dto, Country country)
    {
        return new Hotel
        {
            Name = dto.Name,
            Description = $"{dto.StarRating}-star hotel – Rating {dto.Rating}/10",
            Address = $"{dto.CityCode}, {dto.CountryCode}",
            Country = country,
            City = dto.CityCode,
            Latitude = (decimal)(dto.GeoCode?.Latitude ?? 0),
            Longitude = (decimal)(dto.GeoCode?.Longitude ?? 0),
            //  kan tilføje dto.StarRating til at udvide modellen
        };
    }

    public static HotelStay MapToHotelStay(HotelData dto, Country country, DateTime? checkIn = null, DateTime? checkOut = null, int travellers = 2)
    {
        var checkInDate = checkIn ?? DateTime.Today.AddDays(7);
        var checkOutDate = checkOut ?? DateTime.Today.AddDays(10);

        ISOCurrency iSOCurrency = MatchCurrencyOrDefault(dto.Price.Currency);

        return new HotelStay
        {
            Hotel = MapToHotel(dto, country),
            Price = new Price (dto.Price.Total, iSOCurrency),
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            DaysOfStay = (checkOutDate - checkInDate).Days,
            NoOfTravellers = travellers
        };
    }

    public static ISOCurrency MatchCurrencyOrDefault(string input, ISOCurrency defaultValue = ISOCurrency.DKK)
    {
        if (string.IsNullOrWhiteSpace(input))
            return defaultValue;

        string upper = input.ToUpper();

        if (Enum.TryParse(typeof(ISOCurrency), upper, out var match))
            return (ISOCurrency)match;

        return defaultValue;
    }
}
