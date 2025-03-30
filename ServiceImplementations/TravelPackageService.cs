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
    public class TravelPackageService(AppDbContext context) : CrudService<TravelPackage>(context), ITravelPackageService
    {

    }
}
