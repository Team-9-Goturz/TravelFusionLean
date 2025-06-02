using Shared.Models;
using Shared.Dtos;

namespace ServiceContracts
{
    public interface ITravelPackageService : ICrudService<TravelPackage>
    {
        Task<List<TravelPackage>> SearchAsync(TravelPackageSearchDTO searchDto);
        Task<List<TravelPackage>> SearchAvailableAsync(TravelPackageSearchDTO searchDto);
        Task<IEnumerable<TravelPackage>> GetAvailableAsync();
    }
}
