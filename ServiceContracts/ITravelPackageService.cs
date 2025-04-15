using Shared.Models;
using Shared.Dtos;


namespace ServiceContracts
{
    public interface ITravelPackageService : ICrudService<TravelPackage>
    {
        Task<List<TravelPackage>> SearchAsync(TravelPackageSearchDTO searchDto);

    }
}
