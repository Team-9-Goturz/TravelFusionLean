using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Interface til oprettelse, opdatering og sletning af entiteter.
    /// </summary>
    public interface ICrudService<TEntity> : IReadService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Tilføjer en ny entitet asynkront.
        /// </summary>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Opdaterer en eksisterende entitet asynkront.
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Sletter en entitet ud fra ID.
        /// </summary>
        Task<bool> ArchiveAsync(int id);
    }

}
