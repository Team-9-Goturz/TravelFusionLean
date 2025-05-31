using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IHotelStayService : ICrudService<HotelStay>
    {
        Task<HotelStay> CreateHotelStayAsync(Hotel hotel, HotelStay stay);
        Task<HotelStay> UpdateHotelStayAsync(int id, HotelStay updatedStay);
    }
}
