using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IFlightModelService : ICrudService<Flight>
    {
        Task<Flight> CreateWithAirportsAsync(Flight flight, Airport departure, Airport arrival);
    }
}
