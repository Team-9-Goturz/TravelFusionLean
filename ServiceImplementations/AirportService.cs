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
            Airport? existingAirport;

            if (airport.City != null)
            {
                // Tjekker, om der allerede findes et Airport i databasen med den samme City
                 existingAirport = await context.Airports.FirstOrDefaultAsync(a => a.City == airport.City);
                if (existingAirport != null)
                {
                    // Hvis der findes et eksisterende Airport med den samme City, returneres det eksisterende objekt
                    return existingAirport;
                }
                else
                { // Hvis ikke, tilføjes det nye Airport til databasen
                    await _context.Airports.AddAsync(airport);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                // Hvis ikke, tilføjes det nye Airport til databasen
                await _context.Airports.AddAsync(airport);
                await _context.SaveChangesAsync();
            }
                // Returnerer den nye eller eksisterende Airport
                return airport;
        }

        public async Task<Airport?> GetByIdAsync(int id)
        {
            return await _context.Airports.FindAsync(id);
        }
    }

}
