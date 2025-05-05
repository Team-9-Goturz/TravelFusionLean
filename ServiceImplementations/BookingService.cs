using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                .Include(b => b.Payment)
                .ToListAsync(); // <-- Her!

        }
        public override async Task<Booking> GetByIdAsync(int id)
        {
            Booking booking = _context.Bookings
                .Include(b => b.TravelPackage)
                .FirstOrDefault(u => u.Id == id);
            booking.Status = BookingStatus.Cancelled;
            return await UpdateAsync(booking);
        }
        public async Task<Booking> CancelById(int id)
        {
            Booking booking = await this.GetByIdAsync(id);
            booking.Status = BookingStatus.Cancelled;

            return await this.UpdateAsync(booking);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Booking booking = await GetByIdAsync(id);
                if (booking == null) return false;

                //Slet payment

                _dbSet.Remove(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                Debug.WriteLine($"[DeleteAsync] DB fejl: {dbEx.Message}");
                throw new ApplicationException("Kunne ikke slette entiteten fra databasen.", dbEx);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DeleteAsync] Ukendt fejl: {ex.Message}");
                throw new ApplicationException("Uventet fejl ved sletning.", ex);
            }
        }
    }
}
