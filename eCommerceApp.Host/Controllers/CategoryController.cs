using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Host.Extensions;
using eCommerceApp.Application.Consts;
using Microsoft.AspNetCore.Authorization;
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
            
            if(User.IsInRole(Roles.Admin))
            {
                var Categories = await _categoryService.GetAllAsync();
                return Categories.Any() ? Ok(Categories) : NotFound(Categories);
            }

            var currentCategories = await _categoryService.GetCurrentAsync();
            return currentCategories.Any() ? Ok(currentCategories) : NotFound(currentCategories);
        }

       
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {

            if (User.IsInRole(Roles.Admin))
            {
                var Category = await _categoryService.GetByIdAsync(id);
                return Category != null ? Ok(Category) : NotFound();
            }

            if (User.IsInRole(Roles.User))
            {
                var currentCategory = await _categoryService.GetCurrentByIdAsync(id);
                return currentCategory != null ? Ok(currentCategory) : NotFound();
            }
            return Unauthorized();

        }

        //[HttpGet("products-by-category/{categoryId}")]
        //public async Task<IActionResult> GetProductsByCategory(Guid categoryId)
        //{
        //    var Category = 
        //        await _categoryService.GetProductsByCategoryAsync(categoryId);
        //    return Category.Any() ?  Ok(Category) : NotFound(Category);
        //}

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CreateCategory model)
        {

            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _categoryService.AddAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategory model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _categoryService.UpdateAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);

        }


    }
}
