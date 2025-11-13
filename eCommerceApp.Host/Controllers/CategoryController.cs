using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var Categories = await _categoryService.GetAllAsync();
            return Categories.Any() ? Ok(Categories) : NotFound(Categories);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var Category = await _categoryService.GetByIdAsync(id);
            return Category.Name == null ? NotFound(Category) : Ok(Category);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateCategory model)
        {
            var result = await _categoryService.AddAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategory model)
        {
            var result = await _categoryService.UpdateAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);

        }
    }
}
