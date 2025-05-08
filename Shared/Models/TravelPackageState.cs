using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TravelPackageState
    {
        public Flight? OutboundFlight { get; set; }
        public HotelStay? SelectedHotelStay { get; set;}
        public Flight? InboundFlight { get; set; }
    }
}
