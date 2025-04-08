using System.Text.Json.Serialization;

/// <summary>
/// Data transfer object (Dto) for flydata, tilpasset strukturen fra MockFlightsAPI.
/// </summary>
public class FlightData
{
    [JsonPropertyName("flight_date")]
    public string FlightDate { get; set; }

    [JsonPropertyName("flight_status")]
    public string FlightStatus { get; set; }

    [JsonPropertyName("departure")]
    public FlightLocation Departure { get; set; }

    [JsonPropertyName("arrival")]
    public FlightLocation Arrival { get; set; }

    [JsonPropertyName("airline")]
    public Airline Airline { get; set; }

    [JsonPropertyName("flight")]
    public FlightInfo Flight { get; set; }

    [JsonPropertyName("price")]
    public FlightPrice Price { get; set; }
}

/// <summary>
/// Indeholder oplysninger om enten afgangs- eller ankomstlufthavn.
/// </summary>
public class FlightLocation
{
    [JsonPropertyName("airport")]
    public string Airport { get; set; }

    [JsonPropertyName("timezone")]
    public string TimeZone { get; set; }

    [JsonPropertyName("iata")]
    public string IATA { get; set; }

    [JsonPropertyName("scheduled")]
    public string Scheduled { get; set; }
}

/// <summary>
/// Indeholder information om flyselskabet.
/// </summary>
public class Airline
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("iata")]
    public string IATA { get; set; }

    [JsonPropertyName("icao")]
    public string ICAO { get; set; }
}

/// <summary>
/// Indeholder identificerende oplysninger om flyet.
/// </summary>
public class FlightInfo
{
    [JsonPropertyName("number")]
    public string Number { get; set; }

    [JsonPropertyName("iata")]
    public string IATA { get; set; }

    [JsonPropertyName("icao")]
    public string ICAO { get; set; }
}

/// <summary>
/// Indeholder prisoplysninger for flyrejsen.
/// </summary>
public class FlightPrice
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("classType")]
    public string ClassType { get; set; }
}

/// <summary>
/// Wrapper klasse til at hente flight-listen fra JSON'ens "data" array.
/// </summary>
public class FlightDto
{
    [JsonPropertyName("data")]
    public List<FlightData> Data { get; set; }
}

