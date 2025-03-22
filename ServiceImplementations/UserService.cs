using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using TravelFusionLean.Models;

namespace ServiceImplementations
{
    public class UserService(AppDbContext context) : CrudService<User>(context), IUserService
    {
        public async Task<User> Create(User user, string password)
        {
            string passwordSalt = GenerateSalt();
            string passwordHash = HashPasswordWithSaltAndPepper(password, passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            return await AddAsync(user);
        }

        private static string GenerateSalt(int size = 16) //Ja ja den skal rykkes
        {
            byte[] salt = new byte[size];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string HashPasswordWithSaltAndPepper(string password, string salt) // og den her I know 
        {
            string saltedAndPepperedPassword = password + salt; //+ SecretPepper; TODO


            byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedAndPepperedPassword);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                // Convert to a readable hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}
