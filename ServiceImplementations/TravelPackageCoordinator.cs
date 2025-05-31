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

        public async Task<TravelPackage> CreateTravelpackageAsync(CreateTravelPackageDTO travelpackageDTO, TravelPackageStatus status)
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
                    Status = status,
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
        public async Task<TravelPackage> UpdateTravelpackageAsync(int travelPackageId, CreateTravelPackageDTO travelpackageDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Hent eksisterende TravelPackage fra databasen inkl. relaterede entiteter
                var existingTravelPackage = await _travelPackageService.GetByIdAsync(travelPackageId);
                if (existingTravelPackage == null)
                {
                    throw new KeyNotFoundException($"TravelPackage med Id {travelPackageId} blev ikke fundet.");
                }

                //// 2. Opdater eller find/creat lufthavne for outbound flight
                //if (travelpackageDTO.OutboundFlight != null)
                //{
                //    travelpackageDTO.OutboundFlight.DepartureFromAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.OutboundFlight.DepartureFromAirport);
                //    travelpackageDTO.OutboundFlight.ArrivalAtAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.OutboundFlight.ArrivalAtAirport);

                //    // Opdater outbound flight via flight service
                //    await _flightModelService.UpdateFlightAsync(existingTravelPackage.OutboundFlightId, travelpackageDTO.OutboundFlight);
                //}

                //// 3. Opdater eller find/creat lufthavne for inbound flight
                //if (travelpackageDTO.InboundFlight != null)
                //{
                //    travelpackageDTO.InboundFlight.DepartureFromAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.InboundFlight.DepartureFromAirport);
                //    travelpackageDTO.InboundFlight.ArrivalAtAirport = await _airportService.FindOrCreateAsync(travelpackageDTO.InboundFlight.ArrivalAtAirport);

                //    // Opdater inbound flight via flight service
                //    await _flightModelService.UpdateFlightAsync(existingTravelPackage.InboundFlightId, travelpackageDTO.InboundFlight);
                //}

                //// 4. Opdater eller find/creat hotel for hotel stay
                //if (travelpackageDTO.HotelStay != null)
                //{
                //    travelpackageDTO.HotelStay.Hotel = await _hotelModelService.FindOrCreateAsync(travelpackageDTO.HotelStay.Hotel);

                //    // Opdater hotel stay via hotel stay service
                //    await _hotelstayService.UpdateHotelStayAsync(existingTravelPackage.HotelStayId, travelpackageDTO.HotelStay);
                //}

                // 5. Opdater felter på selve TravelPackage
                existingTravelPackage.Headline = travelpackageDTO.Headline;
                existingTravelPackage.Description = travelpackageDTO.Description;
                existingTravelPackage.Price = travelpackageDTO.Price;
                existingTravelPackage.NoOfTravellers = travelpackageDTO.NoOfTravellers;
                existingTravelPackage.ImagePath = travelpackageDTO.ImagePath;

                // 6. Gem opdateringen via travel package service
                await _travelPackageService.UpdateAsync(existingTravelPackage);

                await transaction.CommitAsync();
                return existingTravelPackage;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
