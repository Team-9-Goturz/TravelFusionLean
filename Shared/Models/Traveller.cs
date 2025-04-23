using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Traveller
    {
        public int Id { get; set; }

        public int BookingId {  get; set; }
        public Booking? Booking { get; set; }
        // Personlige oplysninger
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }

        // Rejsepapirer
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public DateOnly PassportExpiry { get; set; }
        public string PassportIssuingCountry { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        // UI-property 
        [NotMapped]
        public bool ShowDetails { get; set; } = false;
    }
}
