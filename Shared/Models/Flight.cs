using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public decimal price { get; set; }

        public string Airline { get; set; }

        public string ClassType { get; set; }

        public int SeatAvailable { get; set; }

        public DateTime DepatureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int DepatureFromAirportId { get; set; }

        public int ArrivalAtAirportId { get; set; }

        public Airport? ArrivalAtAirport { get; set; }

        public Currency Currency { get; set; }


    }
}
