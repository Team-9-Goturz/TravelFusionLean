using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Data;
using ServiceContracts;
using TravelFusionLean.Models;

namespace ServiceImplementations;

/// <summary>
/// Service der håndterer logik for TravelPackages og inkluderer relaterede data.
/// 
/// </summary>
public class TravelPackageService : CrudService<TravelPackage>, ITravelPackageService
{
    public TravelPackageService(AppDbContext context) : base(context) { }

    /// <summary>
    /// Henter alle rejsepakker med tilhørende fly og hotel.
    /// </summary>
    public override async Task<IEnumerable<TravelPackage>> GetAllAsync()
    {
        return await _context.TravelPackages
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.Currency)
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)

            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.Currency)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)

            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Hotel)
            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Currency)

            .Include(tp => tp.ToHotelTransfer)
                .ThenInclude(f => f.Currency)
            .Include(tp => tp.FromHotelTransfer)
                .ThenInclude(f => f.Currency)

            .ToListAsync();
    }

    /// <summary>
    /// Henter en specifik rejsepakke med relaterede entiteter.
    /// </summary>
    public override async Task<TravelPackage?> GetByIdAsync(int id)
    {
        return await _context.TravelPackages
            .Include(tp => tp.OutboundFlight).ThenInclude(f => f.Currency)
            .Include(tp => tp.OutboundFlight).ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.OutboundFlight).ThenInclude(f => f.DepartureFromAirport)

            .Include(tp => tp.InboundFlight).ThenInclude(f => f.Currency)
            .Include(tp => tp.InboundFlight).ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.InboundFlight).ThenInclude(f => f.DepartureFromAirport)

            .Include(tp => tp.HotelStay).ThenInclude(hs => hs.Hotel)
            .Include(tp => tp.HotelStay).ThenInclude(hs => hs.Currency)

            .Include(tp => tp.ToHotelTransfer).ThenInclude(f => f.Currency)
            .Include(tp => tp.FromHotelTransfer).ThenInclude(f => f.Currency)

            .FirstOrDefaultAsync(tp => tp.Id == id);
    }

    /// <summary>
    /// Søgefunktion til fremtidig implementering af filtrering og brugerdefineret visning.
    /// </summary>
    public List<TravelPackage> Search()
    {
        // Kan udvides med filtreringsparametre som pris, dato, destination osv.
        return new List<TravelPackage>();
    }

    /// <summary>
    /// Opretter en ny rejsepakke.
    /// </summary>
    public override async Task<TravelPackage> AddAsync(TravelPackage travelPackage)
    {
        return await base.AddAsync(travelPackage);
    }

    /// <summary>
    /// Opdaterer en eksisterende rejsepakke.
    /// </summary>
    public override async Task<TravelPackage> UpdateAsync(TravelPackage travelPackage)
    {
        return await base.UpdateAsync(travelPackage);
    }

    /// <summary>
    /// Sletter en rejsepakke.
    /// </summary>
    public override async Task<bool> DeleteAsync(int id)
    {
        return await base.DeleteAsync(id);
    }
}
