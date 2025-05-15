using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    // "Owned" PriceAsDecimal - det betyder, at PriceAsDecimal et value object og ikke har sin egen tabel i databasen, men er en del af Booking
    [Owned]
    public class Price
    {
        public decimal Amount { get; set; }
        public ISOCurrency Currency { get; set; }

        public Price(decimal amount, ISOCurrency currency = ISOCurrency.DKK)
        {
            Amount = amount;
            Currency = currency;
        }
        public override string ToString()
        {
            return $"{Amount:N2} {Currency}";
        }

        public enum ISOCurrency
        {
            DKK, // Dansk Krone
            EUR, // Euro
            USD, // US Dollar
            GBP, // British Pound
            JPY // Japansk Yen
        }
    }
}
