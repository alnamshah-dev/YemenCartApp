using Ecommerce.Application.DTOs.Identity;
using Ecommerce.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUser(CreateUser User);
        Task<LoginResponse> LoginUser(LoginUser User);
        Task<LoginResponse> ReviveToken(string RefreshToken);
    }
}
