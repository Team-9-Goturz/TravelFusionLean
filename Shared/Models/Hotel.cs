using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

/// <summary>
/// Repræsenterer et hotel og dets placering.
/// Matcher databasen: dbo.Hotel
/// </summary>
public class Hotel
{
    [Key]
    public int Id { get; set; }
    public decimal Price { get; set; }

    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [StringLength(600)]
    public string? Description { get; set; }
    
    [StringLength(128)]
    public string Rooms { get; set; }

    [StringLength(128)]
    public string StayingGuests { get; set; }
    public string? Address { get; set; }

    [Column(TypeName = "decimal(9,6)")]
    public decimal Latitude { get; set; }

    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
}
