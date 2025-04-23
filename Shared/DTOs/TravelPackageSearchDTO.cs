using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class TravelPackageSearchDTO
    {
        public string? DepartureLocation { get; set; }     
        public string? Destination { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? NumberOfTravelers { get; set; }         
        public DateOnly? DepartureDateEarliest { get; set; }        
    }
}
