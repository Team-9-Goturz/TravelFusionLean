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
   
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [StringLength(600)]
    public string? Description { get; set; }
    

    public string? Address { get; set; }

    [Column(TypeName = "decimal(9,6)")]
    public decimal Latitude { get; set; }

    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }

    public Country Country { get; set; }
    public string City { get; set; }
    public int Rating;
}
