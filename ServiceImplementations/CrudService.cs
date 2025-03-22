using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace ServiceImplementations
{
    public abstract class CrudService<TEntity> : ReadService<TEntity>, ICrudService<TEntity> where TEntity : class
    {
        protected CrudService(AppDbContext context) : base(context) { }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception exception)
            {
                var i = 1;
                return null;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
