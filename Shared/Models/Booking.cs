using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelFusionLean.Models;

namespace Shared.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int TravelPackageId { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int TravelManagerContactId { get; set; }
        public Contact TravelManagerContact { get; set; }

        public BookingStatus Status { get; set; }
        public DateTime BookingMadeAt { get; set; }

        public int? paymentId { get; set; }
        public Payment? Payment { get; set; }

        public List<Traveller> travellers { get; set;  }


        //public Booking(int travelackageId,User user) // Når brugeren selv er rejseansvarlig
        //{
        //    TravelPackageId = travelackageId;
        //    User = user;
        //    UserId = user.Id;
        //    TravelManagerContactId = (int)user.ContactId;
        //    Status = BookingStatus.Pending;
        //    BookingMadeAt = DateTime.Now;
        //}

        //public Booking(int travelackageId, User user, Contact contact) //Når brugeren ikke selv er rejseansvarlig
        //{
        //    TravelPackageId=travelackageId;
        //    User = user;
        //    UserId = user.Id;
        //    TravelManagerContactId = contact.Id;
        //    Status = BookingStatus.Pending;
        //    BookingMadeAt = DateTime.Now;
        //}
        //public Booking(int travelackageId,Contact contact)// ingen bruger
        //{
        //    TravelPackageId = travelackageId;
        //    TravelManagerContactId = contact.Id;
        //    Status = BookingStatus.Pending;
        //    BookingMadeAt = DateTime.Now;
        //}
    }
    public enum BookingStatus
    {
        Pending,         // Booking er oprettet, men ikke behandlet
        Confirmed,       // Booking er bekræftet og godkendt
        Paid,            // Betaling er modtaget og bekræftet
        Cancelled,       // Booking er aflyst
        Completed,       // Booking er afsluttet
        Refunded         // Betaling er refunderet
    }
}
