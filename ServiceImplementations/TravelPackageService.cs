using Data;
using ServiceContracts;
using ServiceImplementations;
using Shared.Models;
using Microsoft.EntityFrameworkCore;

public class TravelPackageService : CrudService<TravelPackage>, ITravelPackageService
{
    private readonly AppDbContext _context;

    public TravelPackageService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<TravelPackage?> GetByIdAsync(int id)
    {
        return await _context.TravelPackages
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.Currency)

            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.Currency)

            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Hotel)
            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Currency)

            .Include(tp => tp.ToHotelTransfer)
            .Include(tp => tp.FromHotelTransfer)

            .FirstOrDefaultAsync(tp => tp.Id == id);
    }

    public override async Task<IEnumerable<TravelPackage>> GetAllAsync()
    {
        return await _context.TravelPackages
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.Currency)

            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.ArrivalAtAirport)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.Currency)

            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Hotel)
            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Currency)

            .Include(tp => tp.ToHotelTransfer)
            .Include(tp => tp.FromHotelTransfer)
            .ToListAsync();
    }
}
