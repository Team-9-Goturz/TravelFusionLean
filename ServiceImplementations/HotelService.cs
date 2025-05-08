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
    public class HotelService(AppDbContext context) : CrudService<Hotel>(context), IHotelService
    {
        public async Task<Hotel> FindOrCreateAsync(Hotel hotel)
        {
            var existing = await _context.Hotels
                .FirstOrDefaultAsync(h => h.Name == hotel.Name && h.City == hotel.City);

            return existing ?? await AddAsync(hotel);
        }
    }
}
