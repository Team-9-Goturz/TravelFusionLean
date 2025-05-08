using TravelFusionLean.Models;
using Microsoft.EntityFrameworkCore;

namespace Shared.Models
{
    public class Booking
    {
        public int? Id { get; set; }
        public TravelPackage TravelPackage { get; set; }
        public Price Price { get; set; }
        public int TravelPackageId { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int TravelManagerContactId { get; set; }
        public Contact TravelManagerContact { get; set; }

        public Payment? Payment { get; set; }

        public BookingStatus Status { get; set; }
        public DateTime BookingMadeAt { get; set; }
        public DateTime? BookingConfirmedAt { get; set; }
        public DateTime? BookingCancelledAt { get; set; }

        public List<Traveller> travellers { get; set;  }
    }
    public enum BookingStatus
    {
        Pending,         // Booking er oprettet, men ikke behandlet
        Paid,            // Betaling er modtaget og bekræftet
        Confirmed,       // Booking er bekræftet og godkendt af medarbjder der har bekræftet med fly/hotel udbyder
        Completed,       // Booking er afsluttet (kunden kom ud og rejse) 
        Cancelled,       // Booking er aflyst, venter på eventuel refundering
        Archived         // Booking blev aflyst, muligvis refundereret, afsluttet og arkiveret
    }
}
