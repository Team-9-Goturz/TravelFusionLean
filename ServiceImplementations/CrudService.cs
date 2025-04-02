using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Data;
using System.Diagnostics;

namespace ServiceImplementations;

/// <summary>
/// Generisk serviceklasse for CRUD-operationer med forbedret fejlhåndtering og logging.
/// </summary>
public abstract class CrudService<TEntity> : ReadService<TEntity>, ICrudService<TEntity> where TEntity : class
{
    protected CrudService(AppDbContext context) : base(context) { }

    /// <summary>
    /// Opretter en ny entitet i databasen med fejlhåndtering.
    /// </summary>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException dbEx)
        {
            // Logger fejlen (kan udvides med ILogger)
            Debug.WriteLine($"[AddAsync] DB fejl: {dbEx.InnerException?.Message ?? dbEx.Message}");

            // Eventuelt kast custom exception
            throw new ApplicationException("Kunne ikke tilføje entitet til databasen.", dbEx);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[AddAsync] Ukendt fejl: {ex.Message}");
            throw new ApplicationException("Uventet fejl ved tilføjelse.", ex);
        }
    }

    /// <summary>
    /// Opdaterer en eksisterende entitet i databasen.
    /// </summary>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateConcurrencyException conEx)
        {
            Debug.WriteLine($"[UpdateAsync] Concurrency problem: {conEx.Message}");
            throw new ApplicationException("Opdateringskonflikt i databasen.", conEx);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[UpdateAsync] Ukendt fejl: {ex.Message}");
            throw new ApplicationException("Uventet fejl ved opdatering.", ex);
        }
    }

    /// <summary>
    /// Sletter en entitet med det angivne ID.
    /// </summary>
    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException dbEx)
        {
            Debug.WriteLine($"[DeleteAsync] DB fejl: {dbEx.Message}");
            throw new ApplicationException("Kunne ikke slette entiteten fra databasen.", dbEx);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[DeleteAsync] Ukendt fejl: {ex.Message}");
            throw new ApplicationException("Uventet fejl ved sletning.", ex);
        }
    }
}
