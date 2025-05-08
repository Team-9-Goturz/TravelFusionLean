using Shared.Dtos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ITravelpackageCoordinator
    {
        Task<TravelPackage> CreateTravelpackageAsync(CreateTravelPackageDTO travelpackageDTO);
    }
}
