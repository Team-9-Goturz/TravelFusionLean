using ServiceImplementations.Dtos;
using Shared.Models;

namespace ServiceImplementations.Mappers;

/// <summary>
/// Mapper FlightData (fra MockFlightsAPI) til Flight model (EF Core).
/// </summary>
public static class FlightMapper
{
    public static Flight MapToModel(FlightData dto)
    {
        return new Flight
        {
            Airline = dto.Airline?.Name ?? "Unknown",
            ClassType = dto.Price?.ClassType,
            Price = dto.Price?.Amount ?? 0,
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
            },
            Currency = new Currency
            {
                Name = dto.Price?.Currency ?? "EUR"
            }
        };
    }
}