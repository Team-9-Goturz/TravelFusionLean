namespace Shared.Dtos;

/// <summary>
/// Forenklet og ensartet model til at sende flydata til frontend og backend.
/// </summary>
public class FlightDto
{
    public string FlightNumber { get; set; } = string.Empty;
    public string AirlineName { get; set; } = string.Empty;

    public string ClassType { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;

    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public string DepartureAirportCity { get; set; } = string.Empty;
    public string DepartureAirportCountry { get; set; } = string.Empty;
    public decimal DepartureLatitude { get; set; }
    public decimal DepartureLongitude { get; set; }

    public string ArrivalAirportCity { get; set; } = string.Empty;
    public string ArrivalAirportCountry { get; set; } = string.Empty;
    public decimal ArrivalLatitude { get; set; }
    public decimal ArrivalLongitude { get; set; }

    public int SeatsAvailable { get; set; }
}