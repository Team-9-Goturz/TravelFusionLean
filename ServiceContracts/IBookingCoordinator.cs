using Shared.Dtos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IBookingCoordinator
    {
        Task HandleSuccessfulStripePaymentAsync(string stripeSessionId, string stripePaymentIntentId);
        Task<Booking> CreateBookingAsync(CreateBookingDTO bookingDTO);
        Task<string> CreatePaymentAndGetRedirectUrlAsync(Booking booking);
    }
}
