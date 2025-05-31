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
                // 1. Opret flyrejser
                travelpackageDTO.OutboundFlight = await ProcessFlightAsync(travelpackageDTO.OutboundFlight);
                travelpackageDTO.InboundFlight = await ProcessFlightAsync(travelpackageDTO.InboundFlight);

                // 2. Hotel
                travelpackageDTO.HotelStay.Hotel = await _hotelModelService.FindOrCreateAsync(travelpackageDTO.HotelStay.Hotel);
                travelpackageDTO.HotelStay = await _hotelstayService.CreateHotelStayAsync(travelpackageDTO.HotelStay.Hotel, travelpackageDTO.HotelStay);

                // 3. Opret TravelPackage
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
        public async Task<TravelPackage> UpdateTravelpackageAsync(int travelPackageId, CreateTravelPackageDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var travelPackage = await _travelPackageService.GetByIdAsync(travelPackageId)
                    ?? throw new KeyNotFoundException($"TravelPackage med Id {travelPackageId} blev ikke fundet.");

                // 1. Opdater flights
                if (dto.OutboundFlight is not null)
                    travelPackage.OutboundFlight = await UpdateFlightAsync(travelPackage.OutboundFlight, dto.OutboundFlight);

                if (dto.InboundFlight is not null)
                    travelPackage.InboundFlight = await UpdateFlightAsync(travelPackage.InboundFlight, dto.InboundFlight);

                // 2. Opdater hotel og hotelstay
                if (dto.HotelStay is not null)
                {
                    var hotel = await _hotelModelService.FindOrCreateAsync(dto.HotelStay.Hotel);
                    dto.HotelStay.Hotel = hotel;

                    await _hotelstayService.UpdateHotelStayAsync(travelPackage.HotelStay.Id, dto.HotelStay);
                }

                // 3. Opdater TravelPackage-data
                travelPackage.Headline = dto.Headline;
                travelPackage.Description = dto.Description;
                travelPackage.Price = dto.Price;
                travelPackage.NoOfTravellers = dto.NoOfTravellers;
                travelPackage.ImagePath = dto.ImagePath;

                await _travelPackageService.UpdateAsync(travelPackage);
                await transaction.CommitAsync();

                return travelPackage;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<Flight?> ProcessFlightAsync(Flight flight)
        {
            if (flight == null) return flight;

            flight.DepartureFromAirport = await _airportService.FindOrCreateAsync(flight.DepartureFromAirport);
            flight.ArrivalAtAirport = await _airportService.FindOrCreateAsync(flight.ArrivalAtAirport);

            return await _flightModelService.CreateWithAirportsAsync(flight, flight.DepartureFromAirport, flight.ArrivalAtAirport);
        }
        private async Task<Flight> UpdateFlightAsync(Flight existing, Flight updated)
        {
            if (updated == null || existing == null)
                return existing;

            existing.DepartureFromAirport = await _airportService.FindOrCreateAsync(updated.DepartureFromAirport);
            existing.ArrivalAtAirport = await _airportService.FindOrCreateAsync(updated.ArrivalAtAirport);

            existing.DepartureTime = updated.DepartureTime;
            existing.ArrivalTime = updated.ArrivalTime;

            // Hvis der er en owned type som Price eller Baggage, så:
            if (updated.Price != null)
            {
                existing.Price.Amount = updated.Price.Amount;
                existing.Price.Currency = updated.Price.Currency;
            }

            await _flightModelService.UpdateAsync(existing);
            return existing;
        }
        private async Task<HotelStay> UpdateOrCreateHotelStayAsync(HotelStay newHotelStay, HotelStay? existingHotelStay = null)
        {
            if (newHotelStay == null)
                throw new ArgumentNullException(nameof(newHotelStay));

            // Find eller opret hotel
            newHotelStay.Hotel = await _hotelModelService.FindOrCreateAsync(newHotelStay.Hotel);

            if (existingHotelStay == null || existingHotelStay.Id == 0)
            {
                // Ny hotelstay
                return await _hotelstayService.CreateHotelStayAsync(newHotelStay.Hotel, newHotelStay);
            }
            else
            {
                // Opdater eksisterende hotelstay
                existingHotelStay.Hotel = newHotelStay.Hotel;
                existingHotelStay.CheckInDate = newHotelStay.CheckInDate;
                existingHotelStay.CheckOutDate = newHotelStay.CheckOutDate;
                existingHotelStay.Price = newHotelStay.Price;

                await _hotelstayService.UpdateHotelStayAsync(existingHotelStay.Id, existingHotelStay);
                return existingHotelStay;
            }
        }
    }
}
