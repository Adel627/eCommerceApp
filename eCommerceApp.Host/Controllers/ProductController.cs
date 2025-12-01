using eCommerceApp.Application.Consts;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Implementations;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Host.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;


        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {

            if (User.IsInRole(Roles.Admin))
            {
                var Products = await _productService.GetAllAsync();
                return Products.Any() ? Ok(Products) : NotFound(Products);
            }

            var currentProducts = await _productService.GetAllCurrentAsync();
            return currentProducts.Any() ? Ok(currentProducts) : NotFound(currentProducts);
        }


        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {

            if (User.IsInRole(Roles.Admin))
            {
                var Category = await _productService.GetByIdAsync(id);
                return Category != null ? Ok(Category) : NotFound();
            }

            if (User.IsInRole(Roles.User))
            {
                var currentProduct = await _productService.GetCurrentByIdAsync(id);
                return currentProduct != null ? Ok(currentProduct) : NotFound();
            }
            return Unauthorized();

        }

     
      //  [Authorize(Roles = Roles.Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CreateProduct model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _productService.AddAsync(model, null);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        //[Authorize(Roles = Roles.Admin)]
        //[HttpPut("update")]
        //public async Task<IActionResult> Update(UpdateProduct model)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var result = await _productService.UpdateAsync(model);
        //    return result.Success ? Ok(result) : BadRequest(result);
        //}

       // [Authorize(Roles = Roles.Admin)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);

        }


    }
}
