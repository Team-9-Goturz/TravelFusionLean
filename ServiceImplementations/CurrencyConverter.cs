using ServiceContracts;
using Shared.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementations
{
    public class CurrencyConverter : ICurrencyConverter
    {
        // Thread-safe opslagsstruktur til valutakurser
        private readonly ConcurrentDictionary<(Price.ISOCurrency From, Price.ISOCurrency To), decimal> _exchangeRates;

        public CurrencyConverter()
        {
            _exchangeRates = new ConcurrentDictionary<(Price.ISOCurrency, Price.ISOCurrency), decimal>();

            // Eksempelkurser (i virkeligheden kunne de hentes eksternt)
            _exchangeRates[(Price.ISOCurrency.EUR, Price.ISOCurrency.DKK)] = 7.45m;
            _exchangeRates[(Price.ISOCurrency.DKK, Price.ISOCurrency.EUR)] = 1 / 7.45m;

            _exchangeRates[(Price.ISOCurrency.USD, Price.ISOCurrency.DKK)] = 6.80m;
            _exchangeRates[(Price.ISOCurrency.DKK, Price.ISOCurrency.USD)] = 1 / 6.80m;

            _exchangeRates[(Price.ISOCurrency.EUR, Price.ISOCurrency.USD)] = 1.08m;
            _exchangeRates[(Price.ISOCurrency.USD, Price.ISOCurrency.EUR)] = 1 / 1.08m;
        }
        public Price Convert(Price from, Price.ISOCurrency toCurrency)
        {
            if (from.Currency == toCurrency)
                return from;

            if (_exchangeRates.TryGetValue((from.Currency, toCurrency), out var rate))
            {
                decimal convertedAmount = from.Amount * rate;
                return new Price(decimal.Round(convertedAmount, 2), toCurrency);
            }

            throw new ArgumentException($"Exchange rate from {from.Currency} to {toCurrency} not found.");
        }
    }
}
