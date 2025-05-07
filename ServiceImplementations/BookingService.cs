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
                .ToListAsync();
        }
        public override async Task<Booking> GetByIdAsync(int id)
        {
            Booking booking = _context.Bookings
                .Include(b => b.Payment)
                .Include(b => b.TravelPackage)
                .Include(b => b.TravelManagerContact)
                .Include(b => b.travellers)
                    .ThenInclude(t => t.Nationality)
                .Include(b => b.travellers)
                    .ThenInclude(t => t.PassportIssuingCountry)
                .Include(b => b.TravelPackage)
                .FirstOrDefault(u => u.Id == id);
            return await UpdateAsync(booking);
        }
        public async Task<Booking> CancelByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //Find Booking
                var booking = await _context.Bookings
                    .Include(b => b.travellers)
                    .Include(b => b.Payment)
                    .FirstOrDefaultAsync(b => b.Id == id);
                booking.Status = BookingStatus.Cancelled;

                //Frigiv travelpackage
                booking.TravelPackage.Status = TravelPackageStatus.Available;

                //Når booking bliver afbestilt har vi ikke længere hjemmel jævnfør GDPR artikel 6(1)(b) til at opbevare/behandle personoplysningerne 
                _context.Travellers.RemoveRange(booking.travellers);

                //Hvis betaling er gennemført
                if (booking.Payment != null)
                {
                    if (booking.Payment.Status == PaymentStatus.Succeeded)
                    {
                        // Lav refusion (TODO) 
                        booking.Payment.Status = PaymentStatus.Refunded;
                    }
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return booking;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<bool> ArchiveAsync(int id) // arkiver afbestilt booking 
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var booking = await _context.Bookings
                    .Include(b => b.Payment)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (booking == null)
                    return false;

                if (booking.Status == BookingStatus.Cancelled)
                {
                    booking.Status = BookingStatus.Archived;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<Booking> ConfirmByIdAsync(int id) //bekræft at fly og hotel er tilgængeligt
        {
            //Find Booking og opdater status
            var booking = await _context.Bookings
                .Include(b => b.travellers)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == id);
            booking.Status = BookingStatus.Confirmed;
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task MarkBookingAsPaidAsync(int id)
        {
            Booking booking = await GetByIdAsync(id);
            booking.Status = BookingStatus.Paid;
            await UpdateAsync(booking);
        }
    }
}
