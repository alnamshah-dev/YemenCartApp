using Ecommerce.Application.Services.Interfaces.Authentication;
using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<AppUser> _userManager) : IRoleManager
    {
        public async Task<bool> AddUserRole(AppUser user, string userRole)
        {
            var result=await _userManager.AddToRoleAsync(user, userRole);
            return result.Succeeded;
        }

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user= await _userManager.FindByEmailAsync(userEmail); 
            if(user == null) return null;
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
    }
}
