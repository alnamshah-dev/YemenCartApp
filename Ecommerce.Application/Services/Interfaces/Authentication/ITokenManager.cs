using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Authentication
{
    public interface ITokenManager
    {
        List<Claim> GetUserClaimsFromToken(string token);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<string> getUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(string userId, string refreshToken);
        Task<int> updateRefreshToken(string userId, string refreshToken);
        string GenerateToken(List<Claim> claims);
        string GetRefreshToken();
    }
}
