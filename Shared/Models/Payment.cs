using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; } 
        public Currency Currency { get; set; } 
        public string PaymentMethodId { get; set; } // Stripe Payment Method ID
        public string Status { get; set; } // Status for betalingen (f.eks. "succeeded", "failed")
        public string StripePaymentIntentId { get; set; }  // Stripe's Payment Intent ID

    }
}
