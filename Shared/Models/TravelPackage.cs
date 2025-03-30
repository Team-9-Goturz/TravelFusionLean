using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TravelPackage
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int NoOfTravellers { get; set; }

        public string Description { get; set; }

        public int OutBoundFlightId { get; set; }

        public int InBoundFlightId { get; set; }

        public int ToHotelTransferId { get; set; }

        public int FromHotelTransferId { get; set; }

        public Flight? OutBoundFlight { get; set; }

        public Flight? InBoundFlight { get; set; }

        public HotelStay? HotelStay { get; set; }

        public HotelStay? ToHotelTransfer { get; set; }

        public HotelStay? FromHotelTransfer { get; set; }


    }
}
