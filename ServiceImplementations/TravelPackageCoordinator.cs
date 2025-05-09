using Data;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Shared.Dtos;
using Shared.Models;

namespace ServiceImplementations
{
    public class TravelPackageCoordinator : ITravelPackageCoordinator
    {
        private readonly IAirportService _airportService;
        private readonly IFlightModelService _flightModelService;
        private readonly IHotelModelService _hotelModelService;
        private readonly IHotelStayService _hotelstayService;
        private readonly ITravelPackageService _travelPackageService;
        private readonly DbContext _context;

        public TravelPackageCoordinator(AppDbContext context, IAirportService airportService, ITravelPackageService travelPackageService, IHotelStayService hotelStayService, IFlightModelService flightModelService, IHotelModelService hotelModelService)
        {
            _context = context;
            _airportService = airportService;
            _travelPackageService = travelPackageService;
            _hotelstayService = hotelStayService;
            _flightModelService = flightModelService;
            _hotelModelService = hotelModelService;
        }

        public async Task<TravelPackage> CreateTravelpackageAsync(CreateTravelPackageDTO travelpackageDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Opret/få lufthavne
                if (travelpackageDTO.OutboundFlight != null)
                {
                    travelpackageDTO.OutboundFlight.DepartureFromAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.OutboundFlight.DepartureFromAirport);
                    travelpackageDTO.OutboundFlight.ArrivalAtAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.OutboundFlight.ArrivalAtAirport);
                }
                if (travelpackageDTO.InboundFlight != null)
                {
                    travelpackageDTO.InboundFlight.DepartureFromAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.InboundFlight.DepartureFromAirport);
                    travelpackageDTO.InboundFlight.ArrivalAtAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.InboundFlight.ArrivalAtAirport);
                }

                // 2. Opret flyrejser
                travelpackageDTO.OutboundFlight = await _flightModelService.CreateWithAirportsAsync(travelpackageDTO.OutboundFlight, travelpackageDTO.OutboundFlight.DepartureFromAirport, travelpackageDTO.OutboundFlight.ArrivalAtAirport);
                travelpackageDTO.InboundFlight = await _flightModelService.CreateWithAirportsAsync(travelpackageDTO.InboundFlight, travelpackageDTO.InboundFlight.DepartureFromAirport, travelpackageDTO.InboundFlight.ArrivalAtAirport);

                // 3. Hotel
                travelpackageDTO.HotelStay.Hotel = await _hotelModelService.FindOrCreateAsync(travelpackageDTO.HotelStay.Hotel);
                travelpackageDTO.HotelStay = await _hotelstayService.CreateForHotelAsync(travelpackageDTO.HotelStay.Hotel, travelpackageDTO.HotelStay);

                // 4. Opret TravelPackage
                var travelPackage = new TravelPackage
                {
                    OutboundFlight = travelpackageDTO.OutboundFlight,
                    OutboundFlightId = travelpackageDTO.OutboundFlight.Id,

                    InboundFlight = travelpackageDTO.InboundFlight,
                    InboundFlightId = travelpackageDTO.InboundFlight.Id,

                    HotelStay = travelpackageDTO.HotelStay,
                    HotelStayId = travelpackageDTO.HotelStay.Id,

                    Headline = travelpackageDTO.Headline,
                    Description = travelpackageDTO.Description,
                    Price = travelpackageDTO.Price,
                    NoOfTravellers = travelpackageDTO.NoOfTravellers,
                    Status = TravelPackageStatus.Available,
                    ImagePath = travelpackageDTO.ImagePath
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
