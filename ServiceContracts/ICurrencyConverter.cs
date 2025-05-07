using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ICurrencyConverter
    {
        Price Convert(Price from, Price.ISOCurrency toCurrency);
    }
}
