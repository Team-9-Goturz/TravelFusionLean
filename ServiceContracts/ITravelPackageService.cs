using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelFusionLean.Models;

    namespace ServiceContracts
{
    public interface ITravelPackageService : ICrudService<TravelPackage>
    {
        List<TravelPackage> Search();
    }
}
