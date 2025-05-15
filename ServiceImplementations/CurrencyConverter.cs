using ServiceContracts;
using Shared.Models;
using System.Collections.Concurrent;

namespace ServiceImplementations
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly ConcurrentDictionary<(Price.ISOCurrency From, Price.ISOCurrency To), decimal> _exchangeRates;

        public CurrencyConverter()
        {
            _exchangeRates = new ConcurrentDictionary<(Price.ISOCurrency, Price.ISOCurrency), decimal>();

            // Tilføj rater (automatisk i begge retninger)
            AddRate(Price.ISOCurrency.USD, Price.ISOCurrency.DKK, 6.80m);
            AddRate(Price.ISOCurrency.EUR, Price.ISOCurrency.USD, 1.08m);
            AddRate(Price.ISOCurrency.EUR, Price.ISOCurrency.DKK, 7.45m);
            AddRate(Price.ISOCurrency.GBP, Price.ISOCurrency.DKK, 8.60m);
            AddRate(Price.ISOCurrency.GBP, Price.ISOCurrency.EUR, 1.17m);
            AddRate(Price.ISOCurrency.JPY, Price.ISOCurrency.DKK, 0.047m);
            AddRate(Price.ISOCurrency.JPY, Price.ISOCurrency.EUR, 0.0064m);
        }

        private void AddRate(Price.ISOCurrency from, Price.ISOCurrency to, decimal rate)
        {
            _exchangeRates[(from, to)] = rate;
            _exchangeRates[(to, from)] = 1 / rate; // automatisk modsat retning
        }

        public Price Convert(Price from, Price.ISOCurrency toCurrency)
        {
            if (from.Currency == toCurrency)
                return from;

            // Prøv direkte konvertering først
            if (_exchangeRates.TryGetValue((from.Currency, toCurrency), out var rate))
            {
                return new Price(Math.Round(from.Amount * rate, 2), toCurrency);
            }

            // Prøv indirekte konvertering via mellemvaluta
            foreach (var intermediary in Enum.GetValues<Price.ISOCurrency>())
            {
                if (intermediary == from.Currency || intermediary == toCurrency)
                    continue;

                if (_exchangeRates.TryGetValue((from.Currency, intermediary), out var toIntermediaryRate) &&
                    _exchangeRates.TryGetValue((intermediary, toCurrency), out var fromIntermediaryRate))
                {
                    decimal intermediateAmount = from.Amount * toIntermediaryRate;
                    decimal finalAmount = intermediateAmount * fromIntermediaryRate;
                    return new Price(Math.Round(finalAmount, 2), toCurrency);
                }
            }

            throw new ArgumentException($"Exchange rate from {from.Currency} to {toCurrency} not found, either directly or via intermediary.");
        }
    }
}

