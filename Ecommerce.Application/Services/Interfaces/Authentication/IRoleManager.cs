using Ecommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Authentication
{
    public interface IRoleManager
    {
        Task<string?> GetUserRole(string userEmail);
        Task<bool> AddUserRole(AppUser user, string userRole);
    }
}
