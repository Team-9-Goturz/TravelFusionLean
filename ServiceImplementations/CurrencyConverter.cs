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

            // Opdaterede valutakurser (maj/juni 2025)
            AddRate(Price.ISOCurrency.USD, Price.ISOCurrency.DKK, 6.57385m);   // 1 USD = 6.57385 DKK
            AddRate(Price.ISOCurrency.EUR, Price.ISOCurrency.USD, 1.127774m);  // 1 EUR = 1.127774 USD
            AddRate(Price.ISOCurrency.EUR, Price.ISOCurrency.DKK, 7.430m);     // Omregnet: EUR->USD->DKK (ca. 7.43)
            AddRate(Price.ISOCurrency.GBP, Price.ISOCurrency.DKK, 8.812m);     // Omregnet baseret på GBP->EUR->DKK (ca. 8.812)
            AddRate(Price.ISOCurrency.GBP, Price.ISOCurrency.EUR, 1.184998m);  // 1 GBP = 1.184998 EUR
            AddRate(Price.ISOCurrency.JPY, Price.ISOCurrency.DKK, 0.0455m);    // Cirka 1 JPY = 0.0455 DKK (omregnet)
            AddRate(Price.ISOCurrency.JPY, Price.ISOCurrency.EUR, 0.006118m);  // 1 JPY = 0.006118 EUR
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

