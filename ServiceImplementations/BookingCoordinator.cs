using Data;
using ServiceContracts;
using ServiceImplementations.Dtos;
using Shared.Dtos;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementations
{
    public class BookingCoordinator : IBookingCoordinator
    {
        private readonly AppDbContext _context;
        private readonly IPaymentService _paymentService;
        private readonly IBookingService _bookingService;
        private readonly ITravelPackageService _travelPackageService;
        private readonly IContactService _contactService;
        private readonly HttpClient _httpClient;

        public BookingCoordinator(AppDbContext context, IPaymentService paymentService, IBookingService bookingService, ITravelPackageService travelPackageService, IContactService contactService, HttpClient httpClient)
        {
            _context = context;
            _paymentService = paymentService;
            _bookingService = bookingService;
            _travelPackageService = travelPackageService;
            _contactService = contactService;
            _httpClient = httpClient;   
        }

        public async Task<Booking> CreateBookingAsync(CreateBookingDTO bookingDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Booking booking = new Booking
                {
                    TravelPackageId = bookingDTO.TravelPackageId,
                    Status = BookingStatus.Pending,
                    BookingMadeAt = DateTime.Now,
                    travellers = bookingDTO.Travellers.ToList()
                };
                TravelPackage travelPackage = await _travelPackageService.GetByIdAsync(booking.TravelPackageId);
                booking.Price = travelPackage.Price;

                if (bookingDTO.TravelManagerContactId.HasValue)
                {
                    booking.TravelManagerContactId = bookingDTO.TravelManagerContactId.Value;
                }
                else 
                {
                    Contact contact = new Contact();
                    contact.Name = bookingDTO.TravelManagerContact.Name;
                    contact.Email = bookingDTO.TravelManagerContact.Email;
                    contact.PhoneNumber = bookingDTO.TravelManagerContact.PhoneNumber;
                    contact = await _contactService.AddAsync(contact);
                }
                booking = await _bookingService.AddAsync(booking);

                //opdater travelpackage 
                booking.TravelPackage.Status = TravelPackageStatus.Booked;
                await _travelPackageService.UpdateAsync(booking.TravelPackage);

                await transaction.CommitAsync();
                return booking;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task HandleSuccessfulStripePaymentAsync(string stripeSessionId, string stripePaymentIntentId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Find og opdater betaling
                Payment payment = await _paymentService.MarkPaymentAsSucceededAsync(stripeSessionId, stripePaymentIntentId);
                // 2. Find og opdater booking
                await _bookingService.MarkBookingAsPaidAsync(payment.BookingId);
                // 3. Gem ændringer og commit transaktionen
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; // bobler op til controlleren
            }
        }

        public async Task<string> CreatePaymentAndGetRedirectUrlAsync(Booking booking)
        {
            Payment payment = new Payment
            {
                Price = booking.Price,
                BookingId = booking.Id.Value,
                Status = PaymentStatus.Pending,
            };

            payment = await _paymentService.AddAsync(payment);

            var stripeRequest = new StripeCheckoutDTO
            {
                Amount = booking.Price.Amount,
                Currency = booking.Price.Currency.ToString(),
                PackageHeadline = booking.TravelPackage?.Headline ?? "Rejsepakke"
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7274/api/stripe/create-checkout-session", stripeRequest);
            
            if (!response.IsSuccessStatusCode)
                throw new Exception("Stripe request failed.");

            var stripeResponse = await response.Content.ReadFromJsonAsync<StripeSessionResponseDTO>();
            payment.StripeSessionId = stripeResponse.StripeSessionId;
            await _paymentService.UpdateAsync(payment);

            return stripeResponse.Url;
        }

    }
}
