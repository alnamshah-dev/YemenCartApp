using Ecommerce.Application.DTOs.Category;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var data=await categoryService.GetAll();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var data = await categoryService.Get(Id);
            return data!=null ? Ok(data) : NotFound(data);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(CreateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await categoryService.CreateAsync(category);
            return result.Flag?Ok(result):BadRequest(result);
        }
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result=await categoryService.DeleteAsync(Id);
            return result.Flag ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(UpdateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.UpdateAsync(category);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

    }
}
