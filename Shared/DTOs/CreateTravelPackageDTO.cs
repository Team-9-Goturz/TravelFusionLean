using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class CreateTravelPackageDTO
    {
        [StringLength(100)]
        public string? Headline { get; set; }

        public Price Price { get; set; }
        public int NoOfTravellers { get; set; }

        [StringLength(600)]
        public string? Description { get; set; }

        public bool IsRecommended { get; set; }

        // Navigation properties
        public Flight OutboundFlight { get; set; }
        public Flight InboundFlight { get; set; }
        public HotelStay HotelStay { get; set; }
       
        // For at uplade billeder, når man opretter travelpackages
        public string? ImagePath { get; set; }
    }
}
