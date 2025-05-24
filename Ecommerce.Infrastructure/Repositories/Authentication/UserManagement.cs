using Ecommerce.Application.Services.Interfaces.Authentication;
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories.Authentication
{
    public class UserManagement(IRoleManager _roleManager, UserManager<AppUser> _userManager,ApplicationDbContext context) : IUserManager
    {
        public async Task<bool> CreateUser(AppUser user)
        {
            var result=await _userManager.FindByEmailAsync(user.Email!);
            if(result!=null)return false;
            return (await _userManager.CreateAsync(user,user.PasswordHash!)).Succeeded;
        }

        public async Task<IEnumerable<AppUser>?> GetAllUsers()
            => await context.Users.ToListAsync();

        public async Task<AppUser?> GetUserByEmail(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<AppUser?> GetUserById(string userId)
            => await _userManager.FindByIdAsync(userId);

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var user= await _userManager.FindByEmailAsync(email);
            var roleName = await _roleManager.GetUserRole(user!.Email!);
            List<Claim> claims = new List<Claim>
            {
                new Claim("Name",user.Name),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Role,roleName!)
            };
            return claims;

        }

        public async Task<bool> LoginUser(AppUser user)
        {
            var User = await GetUserByEmail(user.Email!);
            if(User== null) return false;
            string? RoleName=await _roleManager.GetUserRole(User!.Email!);
            if(string.IsNullOrEmpty(RoleName)) return false;
            return await _userManager.CheckPasswordAsync(User, user.PasswordHash!);

        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var result=await context.Users.FirstOrDefaultAsync(x => x.Email==email);
            if(result==null) return 0;
             context.Users.Remove(result);
            return await context.SaveChangesAsync();
        }
    }
}
