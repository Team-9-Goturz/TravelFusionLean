namespace Shared.Dtos;

/// <summary>
/// DTO til transport af lufthavnsdata (by og land f.eks.)
/// </summary>
public class AirportDto
{
    public int Id { get; set; }

    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}