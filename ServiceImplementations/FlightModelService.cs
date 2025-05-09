using Data;
using ServiceContracts;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementations
{
    public class FlightModelService(AppDbContext context) : CrudService<Flight>(context), IFlightModelService
    {
        public async Task<Flight> CreateWithAirportsAsync(Flight flight, Airport departure, Airport arrival)
        {
            flight.DepartureFromAirportId = (int)departure.Id;
            flight.ArrivalAtAirportId = (int)arrival.Id;
            return await AddAsync(flight);
        }
    }
}