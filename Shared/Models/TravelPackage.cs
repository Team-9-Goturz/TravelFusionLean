using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelFusionLean.Models
{

    /// <summary>
    /// Repræsenterer en samlet rejsepakke med fly, hotel og transport.
    /// Matcher databasen: dbo.TravelPackage
    /// </summary>
    public class TravelPackage
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(30,2)")]
        public decimal Price { get; set; }

        public int NoOfTravellers { get; set; }

        [StringLength(600)]
        public string? Description { get; set; }

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
        public Flight? ToHotelTransfer { get; set; }
        public Flight? FromHotelTransfer { get; set; }
    }
}