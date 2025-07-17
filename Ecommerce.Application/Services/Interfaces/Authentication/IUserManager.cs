using Ecommerce.Domain.Entities.Identity;
using System.Security.Claims;
//wor
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
