using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
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
            var Products = await _productService.GetAllAsync();
            return Products.Any() ? Ok(Products) : NotFound(Products); 
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product.Name == null ? NotFound(product) : Ok(product);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateProduct model)
        {
           var result = await _productService.AddAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);    
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProduct model)
        {
            var result = await _productService.UpdateAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result =  await _productService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);

        }
    }
}
