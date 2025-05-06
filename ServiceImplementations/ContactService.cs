using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Models;
using TravelFusionLean.Models;

namespace ServiceImplementations
{
    public class ContactService (AppDbContext context) : CrudService<User>(context), IContactService
    {
        public async Task<Contact> AddAsync(Contact entity)
        {
            try
            {
                _context.Contacts.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FEJL: {ex.Message}");
                throw; // evt. throw videre eller returnér null
            }
        }

        public Task<bool> ArchiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contact?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> UpdateAsync(Contact entity)
        {
            throw new NotImplementedException();
        }
    }
}
