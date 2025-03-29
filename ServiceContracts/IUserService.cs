using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelFusionLean.Models;

namespace ServiceContracts
{
    public interface IUserService: ICrudService<User>
    {
        public Task<User> Create(User user, string password);
        public Task<User> Update(User user);

        Task<bool> IsUsernameAvailableAsync(string requestedUsername);

        Task<bool> IsPasswordStrongAsync(string requestedPassword);

        Task<bool> IsEmailValidAsync(string requestedPassword);
        public Task ResetPasswordAsync(int userId, string newPassword);

    }
}
