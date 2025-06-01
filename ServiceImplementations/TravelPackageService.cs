using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Data;
using ServiceContracts;
using Shared.Dtos;
using static Shared.Models.TravelPackage;

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
           .ThenInclude(f => f.DepartureFromAirport) // Eager load udrejse fly og lufthavn
       .Include(tp => tp.HotelStay)
               .ThenInclude(hs => hs.Hotel) //Eager load hotelophold og hotel
       .Include(tp => tp.InboundFlight)
           .ThenInclude(f => f.DepartureFromAirport)///Eager load indrejse fly og lufthavn
           .ToListAsync();
    }
    public async Task<IEnumerable<TravelPackage>> GetAvailableAsync()
    {
        return await _context.TravelPackages
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Hotel)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Where(tp => tp.Status == TravelPackageStatus.Available)
            .ToListAsync(); 
    }



    /// <summary>
    /// Henter en specifik rejsepakke med relaterede entiteter.
    /// </summary>
    public override async Task<TravelPackage?> GetByIdAsync(int id)
    {
        return _context.TravelPackages
           .Include(tp => tp.OutboundFlight)
               .ThenInclude(f => f.DepartureFromAirport) // Eager load udrejse fly og lufthavn
           .Include(tp => tp.HotelStay)
               .ThenInclude(hs => hs.Hotel) //Eager load hotelophold og hotel
           .Include(tp => tp.InboundFlight)
               .ThenInclude(f => f.DepartureFromAirport)//Eager load indrejse fly og lufthavn
            .FirstOrDefault(tp => tp.Id == id);
    }

    /// <summary>
    /// Søgefunktion til fremtidig implementering af filtrering og brugerdefineret visning.
    /// </summary>


    /// <summary>
    /// Opretter en ny rejsepakke.
    /// </summary>
    public override async Task<TravelPackage> AddAsync(TravelPackage travelPackage)
    {

        // Tilføj TravelPackage til DbSet
        await _context.TravelPackages.AddAsync(travelPackage);

        // Gem ændringer i databasen
        await _context.SaveChangesAsync();

        return travelPackage;

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
    public override async Task<bool> ArchiveAsync(int id)
    {
        return await base.ArchiveAsync(id);
    }
    public async Task<List<TravelPackage>> SearchAsync(TravelPackageSearchDTO searchDto)
    {
        var travelPackages = await GetAllAsync();

        var filteredPackages = travelPackages.Where(tp =>
            // Departure location: match på land ELLER by
            (searchDto.DepartureLocation == null ||
             tp.OutboundFlight.DepartureFromAirport.Country.Contains(searchDto.DepartureLocation, StringComparison.OrdinalIgnoreCase) ||
             tp.OutboundFlight.DepartureFromAirport.City.Contains(searchDto.DepartureLocation, StringComparison.OrdinalIgnoreCase)) &&

            // Destination: match på land ELLER by
            (searchDto.Destination == null ||
             tp.HotelStay.Hotel.Country.Name.Contains(searchDto.Destination, StringComparison.OrdinalIgnoreCase) ||
             tp.HotelStay.Hotel.City.Contains(searchDto.Destination, StringComparison.OrdinalIgnoreCase)) &&

             //dato
            (searchDto.DepartureDateEarliest == null ||
             DateOnly.FromDateTime(tp.OutboundFlight.DepartureTime) >= searchDto.DepartureDateEarliest) &&

             //antal rejsende
            (searchDto.NumberOfTravelers == null || searchDto.NumberOfTravelers == 0 ||
             tp.NoOfTravellers == searchDto.NumberOfTravelers) &&
              
             //pris
            (searchDto.MinPrice == null || tp.Price.Amount >= searchDto.MinPrice) &&
            (searchDto.MaxPrice == null || tp.Price.Amount <= searchDto.MaxPrice)
        ).ToList();

        return filteredPackages;
    }
    public async Task<List<TravelPackage>> SearchAvailableAsync(TravelPackageSearchDTO searchDto)
    {
        var query = _context.TravelPackages
            .Include(tp => tp.OutboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Include(tp => tp.HotelStay)
                .ThenInclude(hs => hs.Hotel)
            .Include(tp => tp.InboundFlight)
                .ThenInclude(f => f.DepartureFromAirport)
            .Where(tp => tp.Status == searchDto.Status) 
            .AsQueryable();

        // Departure location: land eller by
        if (!string.IsNullOrWhiteSpace(searchDto.DepartureLocation))
        {
            query = query.Where(tp =>
                tp.OutboundFlight.DepartureFromAirport.Country.ToLower().Contains(searchDto.DepartureLocation.ToLower()) ||
                 tp.OutboundFlight.DepartureFromAirport.City.ToLower().Contains(searchDto.DepartureLocation.ToLower()));
        }

        // Destination: land eller by
        if (!string.IsNullOrWhiteSpace(searchDto.Destination))
        {
            query = query.Where(tp =>
                tp.HotelStay.Hotel.Country.Name.ToLower().Contains(searchDto.Destination.ToLower()) ||
                tp.HotelStay.Hotel.City.ToLower().Contains(searchDto.Destination.ToLower()));
        }

        // Afgangs-dato (tidligst)
        if (searchDto.DepartureDateEarliest != null)
        {
            query = query.Where(tp =>
                DateOnly.FromDateTime(tp.OutboundFlight.DepartureTime) >= searchDto.DepartureDateEarliest);
        }
        // Afgangs-dato (senest)
        if (searchDto.DepartureDateLatest != null)
        {
            query = query.Where(tp =>
                DateOnly.FromDateTime(tp.OutboundFlight.DepartureTime) <= searchDto.DepartureDateLatest);
        }

        // Antal rejsende
        if (searchDto.NumberOfTravelers != null && searchDto.NumberOfTravelers > 0)
        {
            query = query.Where(tp =>
                tp.NoOfTravellers == searchDto.NumberOfTravelers);
        }

        // Prisgrænser
        if (searchDto.MinPrice != null)
        {
            query = query.Where(tp => tp.Price.Amount >= searchDto.MinPrice);
        }

        if (searchDto.MaxPrice != null)
        {
            query = query.Where(tp => tp.Price.Amount <= searchDto.MaxPrice);
        }

        // Kun anbefalede
        if (searchDto.HasToBeRecommended)
        {
            query = query.Where(tp => tp.IsRecommended.Value);
        }

        return await query.ToListAsync();
    }
}
