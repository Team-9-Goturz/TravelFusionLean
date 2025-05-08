using ServiceImplementations.Dtos;
using Shared.Models;
using static Shared.Models.Price;

namespace ServiceImplementations.Mappers;

/// <summary>
/// Mapper FlightData (fra MockFlightsAPI) til Flight model (EF Core).
/// </summary>
public static class FlightMapper
{
    public static Flight MapToModel(FlightData dto)
    {
        ISOCurrency iSOCurrency = MatchCurrencyOrDefault(dto.Price.Currency);
        return new Flight
        {
            Airline = dto.Airline?.Name ?? "Unknown",
            ClassType = dto.Price?.ClassType,
            Price = new Price(dto.Price.Amount, iSOCurrency),
            SeatsAvailable = 100, // Mockværdi

            DepartureTime = DateTime.TryParse(dto.Departure?.Scheduled, out var depTime) ? depTime : DateTime.MinValue,
            ArrivalTime = DateTime.TryParse(dto.Arrival?.Scheduled, out var arrTime) ? arrTime : DateTime.MinValue,

            DepartureFromAirport = new Airport
            {
                City = dto.Departure?.Airport ?? "Unknown",
                Country = "Unknown",
                Latitude = 0,
                Longitude = 0
            },
            ArrivalAtAirport = new Airport
            {
                City = dto.Arrival?.Airport ?? "Unknown",
                Country = "Unknown",
                Latitude = 0,
                Longitude = 0
            }
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