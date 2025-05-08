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
    public class FlightService(AppDbContext context) : CrudService<Flight>(context), IFlightService
    {
        public async Task<Flight> CreateWithAirportsAsync(Flight flight, Airport departure, Airport arrival)
        {
            flight.DepartureFromAirportId = departure.Id;
            flight.ArrivalAtAirportId = arrival.Id;
            return await AddAsync(flight);
        }
    }
}
