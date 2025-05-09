using Data;
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
        public async Task<HotelStay> CreateForHotelAsync(Hotel hotel, HotelStay stay)
        {
            stay.HotelId = hotel.Id;
            return await AddAsync(stay);
        }
    }
}
