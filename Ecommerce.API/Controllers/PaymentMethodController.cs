using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.Services.Interfaces.Cart;
using Ecommerce.Infrastructure.Repositories.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController(IPaymentMethodService _paymethodService) : ControllerBase
    {
        [HttpGet("Payment-Methods")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
        {
            var result=await _paymethodService.GetPaymentMethodsAsync();
            if (!result.Any()) return NotFound();
            return Ok(result);
        }
    }
}
