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

        public List<Traveller> travellers { get; set;  }
    }
    public enum BookingStatus
    {
        Pending,         // Booking er oprettet, men ikke behandlet
        Paid,            // Betaling er modtaget og bekræftet
        Confirmed,       // Booking er bekræftet og godkendt
        Cancelled,       // Booking er aflyst
        Completed,       // Booking er afsluttet
        Refunded         // Betaling er refunderet
    }
}
