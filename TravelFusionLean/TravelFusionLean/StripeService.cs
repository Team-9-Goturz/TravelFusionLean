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

        // Opretter en Checkout-session
        // Opretter en Stripe Checkout session
        public Session CreateCheckoutSession(TravelPackage package)
        {
            // Konverterer prisen til små enheder (øre)
            long amountInCents = (long)(package.Price.Amount * 100);

            // Henter valuta som en streng fra ISOCurrency
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
                        UnitAmount = amountInCents,  // Beløbet i små enheder (øre)
                        Currency = currency,         // Valuta som stræng
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = package.Headline,
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "https://localhost:5001/success",  // Din succes-side URL
                CancelUrl = "https://localhost:5001/cancel",   // Din annullerings-side URL
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }

    }
}
