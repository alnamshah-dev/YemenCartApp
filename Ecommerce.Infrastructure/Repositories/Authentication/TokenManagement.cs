using Ecommerce.Application.Services.Interfaces.Authentication;
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(ApplicationDbContext context, IConfiguration config) : ITokenManager
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            context.RefreshTokens.Add(new RefreshToken
            {
                UserId=userId,
                token = refreshToken
            });
            return await context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(
                    issuer: config["JWT:Issuer"],
                    audience: config["JWT:Audience"],
                    claims:claims,
                    signingCredentials:credential,
                    expires:expiration
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomByte = new byte[byteSize];
            using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomByte);
            }
            string token=Convert.ToBase64String(randomByte);
            return WebUtility.UrlEncode(token);
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler=new JwtSecurityTokenHandler();
            var jwtToken=tokenHandler.ReadJwtToken(token);
            if (jwtToken is not null)
                return jwtToken.Claims.ToList();
            else
                return new List<Claim>();
        }

        public async Task<string> getUserIdByRefreshToken(string refreshToken)=>
              (await context.RefreshTokens.FirstOrDefaultAsync(x => x.token == refreshToken))!.UserId;


        public async Task<int> updateRefreshToken(string userId, string refreshToken)
        {
            var existingToken=await context.RefreshTokens.FirstOrDefaultAsync(x=>x.token == refreshToken);
            if (existingToken == null) return -1;
            existingToken.token = refreshToken;
            return await context.SaveChangesAsync();
        }
        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(x => x.token == refreshToken);
            return user != null;
        }
    }
}
