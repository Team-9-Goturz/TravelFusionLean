using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

/// <summary>
/// Repræsenterer en samlet rejsepakke med fly, hotel og transport.
/// Matcher databasen: dbo.TravelPackage
/// </summary>
public class TravelPackage
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Headline { get; set; }

    [NotMapped]
    public decimal PriceAsDecimal { get; set; }

    public Price? Price { get; set; }
    public int NoOfTravellers { get; set; }

    [StringLength(600)]
    public string? Description { get; set; }
    public TravelPackageStatus Status { get; set; }

    // FK'er
    public int? OutboundFlightId { get; set; }
    public int? InboundFlightId { get; set; }
    public int? HotelStayId { get; set; }
    public int? ToHotelTransferId { get; set; }
    public int? FromHotelTransferId { get; set; }

    // Navigation properties
    public Flight? OutboundFlight { get; set; }
    public Flight? InboundFlight { get; set; }
    public HotelStay? HotelStay { get; set; }
    public Transfer? ToHotelTransfer { get; set; }
    public Transfer? FromHotelTransfer { get; set; }


    // For at uplade billeder, når man opretter travelpackages
    public string ImagePath { get; set; }

    public enum TravelPackageStatus
    {
        Draft,          // Endnu ikke klar til booking (kun intern brug)
        Available,      // Klar til booking
        Booked,         // Allerede booket af én kunde (ved 1:1 forhold)
        Cancelled       // Tilbuddet er aflyst
    }
}