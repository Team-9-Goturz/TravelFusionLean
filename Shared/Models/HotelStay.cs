using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class HotelStay
    {

        [Key]
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int DaysOfStay { get; set; }

        public int NoOfTravellers { get; set; }

        public Hotel? Hotel { get; set; }

        public Currency? Currency { get; set; }
    }
}
