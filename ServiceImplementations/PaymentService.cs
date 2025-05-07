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
    }
}
