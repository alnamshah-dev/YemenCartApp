using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService _cartService) : ControllerBase
    {
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(Checkout checkout)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result=await _cartService.CheckOut(checkout);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Save-Checkout")]
        public async Task<IActionResult> SavingCheckout(IEnumerable<CreateAchieve> checkout)
        {
            var result=await _cartService.SaveCheckoutHistory(checkout);
            return result.Flag?Ok(result) : BadRequest(result);
        }
    }
}
