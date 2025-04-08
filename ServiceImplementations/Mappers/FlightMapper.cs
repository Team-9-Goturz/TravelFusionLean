using Shared.Dtos;
using TravelFusionLeanApi.Models; // Hvis dine FlightData-klasser ligger her

public static class FlightMapper
{
    public static FlightDto ToDto(FlightData data)
    {
        return new FlightDto
        {
            FlightNumber = data.Flight.Number,
            AirlineName = data.Airline.Name,
            ClassType = data.Price.ClassType,
            Price = data.Price.Amount,
            Currency = data.Price.Currency,
            DepartureTime = DateTime.Parse(data.Departure.Scheduled),
            ArrivalTime = DateTime.Parse(data.Arrival.Scheduled),
            DepartureAirportCity = data.Departure.Airport,
            DepartureAirportCountry = "DK", // Mock data har ikke land - evt. hardcodet
            DepartureLatitude = 0, // Hvis ikke givet i mock
            DepartureLongitude = 0,
            ArrivalAirportCity = data.Arrival.Airport,
            ArrivalAirportCountry = "FR", // eksempel
            ArrivalLatitude = 0,
            ArrivalLongitude = 0,
            SeatsAvailable = 100 // eksempelværdi
        };
    }
}