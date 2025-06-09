using Data;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;

namespace ServiceImplementations
{
    public class HotelModelService(AppDbContext context): CrudService<Hotel>(context), IHotelModelService
    {
        public override async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
           .Include(tp => tp.Country)
               .ToListAsync();
        }
        public async Task<Hotel> FindOrCreateAsync(Hotel hotel)
        {
            var existing = await _context.Hotels
                .FirstOrDefaultAsync(h => h.Name == hotel.Name && h.City == hotel.City);

            return existing ?? await AddAsync(hotel);
        }

    }
}

