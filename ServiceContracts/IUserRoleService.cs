using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelFusionLean.Models;

namespace ServiceContracts
{
    /// <summary>
    /// Interface til læsning af brugerroller.
    /// </summary>
    public interface IUserRoleService: IReadService<UserRole>
    {
    }
}
