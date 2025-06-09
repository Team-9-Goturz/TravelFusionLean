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
            try
            {
                if (_dbSet == null)
                    throw new InvalidOperationException("DbSet is not initialized.");

                return await _dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FEJL i GetAllAsync(): {ex.Message}");
                throw; // Eller returnér en tom liste for at undgå crash
            }
        }
    }

}
