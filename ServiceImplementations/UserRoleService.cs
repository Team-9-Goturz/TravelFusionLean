using Data;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelFusionLean.Models;

namespace ServiceImplementations
{
    public class UserRoleService(AppDbContext context) : ReadService<UserRole>(context), IUserRoleService
    {
    }
}
