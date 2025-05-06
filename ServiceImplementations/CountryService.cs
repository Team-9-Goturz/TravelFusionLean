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
    }
}


