using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ContactDTO
    {
        [Required(ErrorMessage = "Navn er påkrævet.")]
        [RegularExpression(@"^[\p{L} \-']+$", ErrorMessage = "Navnet må kun indeholde bogstaver, bindestreger og mellemrum.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefonnummer er påkrævet.")]
        [RegularExpression(@"^(?:\+|00)?[0-9\s\-]{6,20}$", ErrorMessage = "Ugyldigt telefonnummer.")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "EmailForPasswordReset er påkrævet.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Ugyldig email.")]
        public string Email { get; set; }

        public ContactDTO(string name, string phoneNumber, string email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
