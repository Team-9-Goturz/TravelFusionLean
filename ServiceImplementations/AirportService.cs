using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementations
{
    public class AirportService(AppDbContext context) : CrudService<Airport>(context), IAirportService
    {
        public async Task<Airport> FindOrCreateAsync(Airport airport)
        {
            Airport existing = await context.Airports.Where(a => a.Name == airport.Name).FirstOrDefaultAsync();


            if (existing != null)
                return existing;

            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();
            return airport;
        }

        public async Task<Airport?> GetByIdAsync(int id)
        {
            return await _context.Airports.FindAsync(id);
        }
    }

}
