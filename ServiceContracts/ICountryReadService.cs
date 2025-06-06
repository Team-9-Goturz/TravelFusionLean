﻿using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ICountryReadService: IReadService<Country>
    {
        Task<Country?> GetByCountryCodeAsync(string landCode);
        Task<Dictionary<string, Country>> GetCountriesByCodeAsync(IEnumerable<string> countryCodes);
    }
}
