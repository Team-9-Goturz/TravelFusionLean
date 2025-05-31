using Shared.Dtos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ITravelPackageCoordinator
    {
        Task<TravelPackage> CreateTravelpackageAsync(CreateTravelPackageDTO travelpackageDTO, TravelPackageStatus status);
        Task<TravelPackage> UpdateTravelpackageAsync(int travelPackageId, CreateTravelPackageDTO travelpackageDTO);
    }
}
