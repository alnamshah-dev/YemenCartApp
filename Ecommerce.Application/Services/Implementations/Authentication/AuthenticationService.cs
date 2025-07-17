using AutoMapper;
using Ecommerce.Application.DTOs.Identity;
using Ecommerce.Application.DTOs.Response;
using Ecommerce.Application.Services.Interfaces.Authentication;
using Ecommerce.Application.Services.Interfaces.Logging;
using Ecommerce.Application.Validation;
using Ecommerce.Application.Validation.Authentication;
using Ecommerce.Domain.Entities.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(ITokenManager _tokenManager,IUserManager _userManager,IRoleManager _roleManager,IMapper _map,IValidator<CreateUser> _createUserValidator,IValidator<LoginUser> _LoginUserValidator,IAppLogger<AuthenticationService> _logger,IValidationService _validationService) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser User)
        {
            var validationResult=await _validationService.ValidateAsync(User,_createUserValidator);
            if (!validationResult.Flag) return validationResult;
            var mapData=_map.Map<AppUser>(User);
            mapData.UserName=User.Email;
            mapData.PasswordHash = User.Password;
            var result=await _userManager.CreateUser(mapData);
            if (!result)
                return new ServiceResponse { message = "Email address might be used or unknown error occurred." };
            var user = await _userManager.GetUserByEmail(User.Email);
            var users = await _userManager.GetAllUsers();
            var AssignedResult = await _roleManager.AddUserRole(user!, users!.Count() > 1 ? "User" : "Admin");
            if (!AssignedResult)
            {
                int removeResult = await _userManager.RemoveUserByEmail(user!.Email!);
                if (removeResult <= 0)
                {
                    _logger.LogError(new Exception($"User with email as {user.Email} failed to be removed as a result of role assigning issue."), "User Could not be assigned to role.");
                    return new ServiceResponse { message = "Error occurred in account creation." };
                }
            }
            return new ServiceResponse { Flag = true, message = "Account created successfully" };

        }
        public async Task<LoginResponse> LoginUser(LoginUser User)
        {

            var validationResult = await _validationService.ValidateAsync(User, _LoginUserValidator);
            if (!validationResult.Flag) return new LoginResponse(Messge: validationResult.message);
            var mapData =_map.Map<AppUser>(User);
            mapData.PasswordHash = User.Password;
            bool loginResult=await _userManager.LoginUser(mapData);
            if (!loginResult) return new LoginResponse(Messge: "Email not found or invalid credential.");
            var user = await _userManager.GetUserByEmail(User.Email);
            var claims=await _userManager.GetUserClaims(User.Email);
            string jwtToken = _tokenManager.GenerateToken(claims);
            string refreshToken = _tokenManager.GetRefreshToken();
            int saveTokenResult = 0;
            bool userTokenCheck = await _tokenManager.ValidateRefreshToken(refreshToken);
            if (userTokenCheck)
                saveTokenResult = await _tokenManager.updateRefreshToken(user!.Id, refreshToken);
            else
                saveTokenResult = await _tokenManager.AddRefreshToken(user!.Id, refreshToken);

            return saveTokenResult <= 0 ? new LoginResponse(Messge: "Internal error occurred while authenticating"):new LoginResponse(Flag:true,jwtToken,RefreshToken:refreshToken);

        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            var validationToken=await _tokenManager.ValidateRefreshToken(refreshToken);
            if (!validationToken) return new LoginResponse(Messge: "Invalid token");
            string userId =await  _tokenManager.getUserIdByRefreshToken(refreshToken);
            AppUser? user=await _userManager.GetUserById(userId);
            var claims=await _userManager.GetUserClaims(user!.Email!);
            string newJwtToken = _tokenManager.GenerateToken(claims);
            string newRefreshToken = _tokenManager.GetRefreshToken();
            await _tokenManager.updateRefreshToken(userId, newRefreshToken);
            return new LoginResponse(Flag: true, Token: newJwtToken, RefreshToken : newRefreshToken);
        }
    }
}
