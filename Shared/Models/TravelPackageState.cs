using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TravelPackageState
    {
        public Flight? SelectedFlight { get; set; }
        public HotelStay? SelectedHotelStay { get; set;}
    }
}
