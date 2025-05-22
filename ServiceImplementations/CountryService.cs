using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Models;

namespace ServiceImplementations
{
     public class CountryReadService : ReadService<Country>, ICountryReadService
    {
        public CountryReadService(AppDbContext context) : base(context)
        {

        }

        public async  Task<Country?> GetByCountryCodeAsync(string iso2Code)
        {
            return await _context.Countries
           .FirstOrDefaultAsync(c => c.Iso2Code == iso2Code);
        }

        public async Task<Dictionary<string, Country>> GetCountriesByCodeAsync(IEnumerable<string> countryCodes)
        {
            return await _context.Countries
                .Where(c => countryCodes.Contains(c.Iso2Code))
                .ToDictionaryAsync(c => c.Iso2Code);
        }
    }
}


