using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

/// <summary>
/// Repræsenterer en lufthavn med placering.
/// Matcher databasen: dbo.Airport
/// </summary>
public class Airport
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    [Required]
    [StringLength(128)]
    public string Country { get; set; }

    [Required]
    [StringLength(128)]
    public string City { get; set; }

    [StringLength(128)]
    public string? Address { get; set; }

    [Column(TypeName = "decimal(9,6)")]
    public decimal Latitude { get; set; }

    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
}
