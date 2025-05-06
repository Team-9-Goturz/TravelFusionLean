using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IBookingService : ICrudService<Booking>
    {
        public Task<Booking> CancelByIdAsync(int id);
        public Task<Booking> ConfirmByIdAsync(int id);
    }
}
