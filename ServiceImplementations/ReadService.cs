using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace ServiceImplementations
{
    /// <summary>
    /// Abstrakt serviceklasse til generelle læseoperationer (get by ID og get all).
    /// </summary>
    public abstract class ReadService<TEntity> : IReadService<TEntity> where TEntity : class
    {
        /// <summary>
        /// EF Core databasekontekst.
        /// </summary>
        protected readonly AppDbContext _context;

        /// <summary>
        /// DbSet for den pågældende entitet.
        /// </summary>
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initialiserer læseservice med databasekontekst.
        /// </summary>
        protected ReadService(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Henter en entitet ud fra dens ID.
        /// </summary>
        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        /// <summary>
        /// Henter alle entiteter af typen.
        /// </summary>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }

}
