using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

/// <summary>
/// Repræsenterer et hotelophold med pris, datoer og kobling til hotel.
/// Matcher databasen: dbo.HotelStay
/// </summary>
public class HotelStay
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(30,2)")]
    public decimal Price { get; set; }

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public int DaysOfStay { get; set; }
    public int NoOfTravellers { get; set; }

    public int HotelId { get; set; }
    public int CurrencyId { get; set; }

    public Hotel? Hotel { get; set; }
    public Currency? Currency { get; set; }
}
