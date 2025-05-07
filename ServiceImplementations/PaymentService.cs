using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelFusionLean.Models;

namespace ServiceImplementations
{
    public class PaymentService(AppDbContext context) : CrudService<Payment>(context), IPaymentService
    {
        public async Task<Payment> GetPaymentByStripeSessionIdAsync(string sessionId)
        {
            return await context.Payments
                             .FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
        }
        public async Task<Payment> MarkPaymentAsSucceededAsync(string stripeSessionId, string stripePaymentIntentId)
        {
            Payment payment = await GetPaymentByStripeSessionIdAsync(stripeSessionId);

            // Opdater betalingens status
            payment.StripePaymentIntentId = stripePaymentIntentId;
            payment.Status = PaymentStatus.Succeeded; // Set status til Succeeded
            await this.UpdateAsync(payment);
            return payment;
        }
    }
}
