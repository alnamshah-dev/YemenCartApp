using Ecommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Authentication
{
    public interface IUserManager
    {
        Task<bool> CreateUser(AppUser user);
        Task<bool> LoginUser(AppUser user);
        Task<AppUser?> GetUserById(string userId);
        Task<AppUser?> GetUserByEmail(string email);
        Task<IEnumerable<AppUser>?> GetAllUsers();
        Task<int> RemoveUserByEmail(string email);
        Task<List<Claim>> GetUserClaims(string email);

    }
}
