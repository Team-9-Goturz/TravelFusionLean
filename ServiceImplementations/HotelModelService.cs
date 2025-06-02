using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Models;

namespace ServiceImplementations
{
    public class HotelModelService(AppDbContext context): CrudService<Hotel>(context), IHotelModelService
    {
        public async Task<Hotel> FindOrCreateAsync(Hotel hotel)
        {
            var existing = await _context.Hotels
                .FirstOrDefaultAsync(h => h.Name == hotel.Name && h.City == hotel.City);

            return existing ?? await AddAsync(hotel);
        }
    }
}

