using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Interface til at hente en eller flere entiteter fra databasen.
    /// </summary>
    public interface IReadService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Henter en entitet ud fra dens unikke ID.
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Henter alle entiteter.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();
    }

}
