using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

/// <summary>
/// Repræsenterer en flyvning, inklusive afgang, ankomst og valuta.
/// Matcher databasen: dbo.Flight
/// </summary>
public class Flight
{
    [Key]
    public int? Id { get; set; }

    [Column(TypeName = "decimal(30,2)")]
    public Price Price { get; set; }

    [Required]
    [StringLength(128)]
    public string Airline { get; set; }

    [StringLength(50)]
    public string? ClassType { get; set; }

    public int SeatsAvailable { get; set; }

    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public int DepartureFromAirportId { get; set; }
    public int ArrivalAtAirportId { get; set; }

    // Navigation properties
    public Airport? ArrivalAtAirport { get; set; }
    public Airport? DepartureFromAirport { get; set; }
    
}
