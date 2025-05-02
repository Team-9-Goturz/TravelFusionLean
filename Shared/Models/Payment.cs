using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Payment
    {
        public int? Id { get; set; }
        public Price Price { get; set; }
        public PaymentStatus Status { get; set; } // Status for betalingen 

        //Stripe relateret
        public string? StripePaymentIntentId { get; set; }  // Stripe's Payment Intent ID
        public string? StripeSessionId { get; set; }

        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        public enum PaymentStatus
        {
            Pending,        // Betalingen er endnu ikke afsluttet
            Succeeded,      // Betalingen er gennemført med succes
            Failed,         // Betalingen mislykkedes
            Canceled,       // Betalingen blev annulleret
            Expired,        // Checkout session udløb uden betaling
            Refunded,       // Hele beløbet blev refunderet
        }
    }
}
