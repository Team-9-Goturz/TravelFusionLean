using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Dtos;
using Shared.Models;

namespace ServiceImplementations
{
    public class TravelPackageCoordinator : ITravelpackageCoordinator
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IHotelService _hotelService;
        private readonly IHotelStayService _hotelstayService;
        private readonly ITravelPackageService _travelPackageService;
        private readonly DbContext _context;

        public async Task<TravelPackage> CreateTravelpackageAsync(CreateTravelPackageDTO travelpackageDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Opret/få lufthavne
                Airport departureAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.OutboundFlight.DepartureFromAirport);
                Airport arrivalAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.OutboundFlight.ArrivalAtAirport);

                Airport returnDepartureAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.InboundFlight.DepartureFromAirport);
                Airport returnArrivalAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.InboundFlight.ArrivalAtAirport);

                // 2. Opret flyrejser
                Flight outboundFlight = await _flightService.CreateWithAirportsAsync(travelpackageDTO.OutboundFlight, departureAirport, arrivalAirport);
                Flight inboundFlight = await _flightService.CreateWithAirportsAsync(travelpackageDTO.InboundFlight, returnDepartureAirport, returnArrivalAirport);

                // 3. Hotel
                Hotel hotel = await _hotelService.FindOrCreateAsync(travelpackageDTO.HotelStay.Hotel);
                HotelStay hotelStay = await _hotelstayService.CreateForHotelAsync(hotel, travelpackageDTO.HotelStay);

                // 4. Opret TravelPackage
                var travelPackage = new TravelPackage
                {
                    OutboundFlight = outboundFlight,
                    InboundFlight = inboundFlight,
                    HotelStay = hotelStay,
                    Headline = travelpackageDTO.Headline,
                    Description = travelpackageDTO.Description,
                    Price = travelpackageDTO.Price,
                    Status = TravelPackageStatus.Available
                };

                await _travelPackageService.AddAsync(travelPackage);
                await transaction.CommitAsync();

                return travelPackage;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
