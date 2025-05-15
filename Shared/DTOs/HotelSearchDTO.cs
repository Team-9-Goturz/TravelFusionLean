using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class HotelSearchDTO
    {
        public string? SearchCountry;
        public string? SearchCity;
        public DateTime? SearchCheckinDate;
        public DateTime? SearchCheckOutDate;
        public decimal? SearchPrice;
        public int? SearchNumberOfTravellers;
    }
}
