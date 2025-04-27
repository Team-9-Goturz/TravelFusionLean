using Microsoft.Extensions.Options;
using Stripe;
using static System.Collections.Specialized.BitVector32;
using TravelFusionLean;
using Shared.Models;
using Stripe.Checkout;
namespace ServiceImplementations
{
    public class StripeService
    {
        private readonly StripeSettings _stripeSettings;

        public StripeService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;  // Sæt Stripe's Secret Key
        }

        // Opretter en Stripe Checkout session
        public async Task<Session> CreateCheckoutSessionAsync(TravelPackage package)
        {
            try
            {
                long amountInCents = (long)(package.Price.Amount * 100);
                string currency = package.Price.Currency.ToString().ToLower();

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = amountInCents,
                        Currency = currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = package.Headline,
                        },
                    },
                    Quantity = 1,
                },
            },
                    Mode = "payment",
                    SuccessUrl = "https://localhost:7177/success",
                    CancelUrl = "https://localhost:7177/cancel",
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options); // Brug CreateAsync i stedet for Create
                return session;
            }
            catch (StripeException ex)
            {
                // Log stripe fejl og giv brugeren feedback
                Console.WriteLine($"Stripe error: {ex.Message}");
                throw new ApplicationException("Der opstod en fejl ved oprettelse af Stripe Checkout session.");
            }
            catch (Exception ex)
            {
                // Håndter fejl
                Console.WriteLine($"Fejl: {ex.Message}");
                throw;
            }
        }

    }
}
