using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Models;
using TravelFusionLean.Models;

namespace ServiceImplementations
{
    public class UserService(AppDbContext context) : CrudService<User>(context), IUserService
    {
        private const int MinPasswordLength = 8;

        public async Task<User> Create(User user, string password)
        {
            try
            {
                string passwordSalt = GenerateSalt();
                string passwordHash = HashPasswordWithSaltAndPepper(password, passwordSalt);

                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;

                if (user.Contact == null)
                    throw new ArgumentException("Contact is required.");

                // Gem Contact først
                _context.Contacts.Add(user.Contact);
                await _context.SaveChangesAsync();

                // Brug ContactId
                user.ContactId = user.Contact.Id;

                // Gem bruger
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                // Log til konsol
                Console.WriteLine("🔥 FEJL i UserService.Create:");
                Console.WriteLine(ex.ToString());

                // Evt. log til fil/db hvis ønsket

                // Genkast for at Blazor/kalder kan reagere
                throw;
            }
        }
        public async override Task<User?> GetByIdAsync(int id)
        {
            return _context.Users
            .Include(u => u.UserRole)
            .Include(u => u.Contact)
            .FirstOrDefault(u => u.Id == id);
        }

        public async Task<bool> IsUsernameAvailableAsync(string requestedUsername)
        {
            return !await _context.Users.AnyAsync(u => u.Username == requestedUsername);
        }

        public async Task<bool> IsPasswordStrongAsync(string requestedPassword)
        {
            //TODO flere krav til adgangskoder som store/små bogstaver? tal? specialtegn?
            return (requestedPassword.Length >= MinPasswordLength);
        }

        public async Task<bool> IsEmailValidAsync(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
        public async Task ResetPasswordAsync(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            string salt = GenerateSalt();
            string hash = HashPasswordWithSaltAndPepper(newPassword, salt);

            user.PasswordSalt = salt;
            user.PasswordHash = hash;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        private static string GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string HashPasswordWithSaltAndPepper(string password, string salt) 
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

        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);          
            await _context.SaveChangesAsync();        
            return user;
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return _context.Users
           .Include(u => u.UserRole) // Eager load rollen
           .Include(u => u.Contact)
           .ToList();
        }


        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.EmailForPasswordReset == email);

            if (user == null)
                return null;

            string attemptedHash = HashPasswordWithSaltAndPepper(password, user.PasswordSalt);

            if (user.PasswordHash == attemptedHash)
                return user;

            return null;
        }

        public async Task<User?> AuthenticateByUsernameAsync(string username, string password)
        {
            var user = await _context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            string attemptedHash = HashPasswordWithSaltAndPepper(password, user.PasswordSalt);

            if (user.PasswordHash == attemptedHash)
                return user;

            return null;
        }

      
    }
}
