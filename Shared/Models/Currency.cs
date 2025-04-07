using System.ComponentModel.DataAnnotations;

namespace TravelFusionLean.Models
{

    /// <summary>
    /// Repræsenterer en valuta.
    /// Matcher databasen: dbo.Currency
    /// </summary>
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}