using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class CreateBookingDTO
    {
        public int TravelPackageId { get; set; }
        public List<Traveller> Travellers { get; set; }
        public int? TravelManagerContactId { get; set; }
        public ContactDTO? TravelManagerContact { get; set; }

        //Når brugeren selv vil være rejseansvarlig
        public CreateBookingDTO(int travelPackageId,List<Traveller> travellers, int contactId)
        {
            TravelPackageId = travelPackageId;
            Travellers = travellers;
            TravelManagerContactId = contactId;
        }
        //Når brugeren opretter en rejseansvarlig
        public CreateBookingDTO(int travelPackageId, List<Traveller> travellers, ContactDTO contact)
        {
            TravelPackageId = travelPackageId;
            Travellers = travellers;
            TravelManagerContact = contact;
        }

    }

}
