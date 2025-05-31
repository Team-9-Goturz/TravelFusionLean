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
    public class HotelStayService(AppDbContext context) : CrudService<HotelStay>(context), IHotelStayService
    {
        public async Task<HotelStay> CreateHotelStayAsync(Hotel hotel, HotelStay stay)
        {
            stay.HotelId = hotel.Id;
            return await AddAsync(stay);
        }
        /// <summary>
        /// Opdaterer en eksisterende HotelStay, inkl. reference til hotel
        /// </summary>
        public async Task<HotelStay> UpdateHotelStayAsync(int id, HotelStay updatedStay)
        {
            if (updatedStay == null)
                throw new ArgumentNullException(nameof(updatedStay));

            var existingStay = await _context.HotelStays
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (existingStay == null)
                throw new KeyNotFoundException($"HotelStay med Id {id} blev ikke fundet.");

            // Opdater felter
            existingStay.CheckInDate = updatedStay.CheckInDate;
            existingStay.CheckOutDate = updatedStay.CheckOutDate;
            existingStay.Price = updatedStay.Price;

            if (updatedStay.Hotel != null)
            {
                existingStay.HotelId = updatedStay.Hotel.Id;
            }

            await _context.SaveChangesAsync();
            return existingStay;
        }
    }
}

