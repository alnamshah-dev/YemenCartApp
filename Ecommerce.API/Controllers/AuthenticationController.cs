using Ecommerce.Application.DTOs.Identity;
using Ecommerce.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("Create-User")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var result=await _authenticationService.CreateUser(user);
            return result.Flag ? Ok(result):BadRequest(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginUser User)
        {
            var result=await _authenticationService.LoginUser(User);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
        [HttpPost("RefreshToken/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _authenticationService.ReviveToken(refreshToken);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

    }
}
