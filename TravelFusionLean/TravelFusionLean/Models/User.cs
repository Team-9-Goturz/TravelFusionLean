namespace TravelFusionLean.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string passwordHash { get; set; }
        public string passwordSalt { get; set; }

        public string email { get; set; }

        public UserRole UserRole { get; set; }


    }
}
