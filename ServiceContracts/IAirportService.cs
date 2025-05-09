using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IAirportService : ICrudService<Airport>
    {
        Task<Airport> FindOrCreateAsync(Airport airport);
        Task<Airport?> GetByIdAsync(int id);
    }
}
