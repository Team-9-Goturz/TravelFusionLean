using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Models;
using TravelFusionLean.Models;

namespace ServiceImplementations
{
    public class BookingService(AppDbContext context) : CrudService<Booking>(context), IBookingService
    {
        public override async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.TravelPackage)
                .ToListAsync(); // <-- Her!

        }
    }
}
