using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace TravelFusionLean.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "Email er påkrævet.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Ugyldig email.")]
        public string EmailForPasswordReset { get; set; }

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public Contact? Contact { get; set; }
        public int? ContactId { get; set; }


        public User()
        {
            Contact = new Contact();
        }
    }
}
