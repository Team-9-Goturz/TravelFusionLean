namespace Shared.Dtos;

/// <summary>
/// Forenklet model til hoteldata, baseret på MockHotelsAPI.
/// </summary>
public class HotelDto
{
    public string HotelId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CityCode { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;

    public int StarRating { get; set; }
    public double Rating { get; set; }

    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;

    public bool Available { get; set; }
    public List<string> Images { get; set; } = new();

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}