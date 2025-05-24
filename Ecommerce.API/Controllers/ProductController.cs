using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _product) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> All()
        {
            var data=await _product.GetAll();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var data = await _product.Get(Id);
            return data!=null? Ok(data) : NotFound(data);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _product.CreateAsync(product);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await _product.UpdateAsync(product);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result=await _product.DeleteAsync(Id);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
    }
}
