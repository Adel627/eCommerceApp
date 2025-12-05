using Microsoft.AspNetCore.Mvc;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Host.Extensions;
using Microsoft.AspNetCore.Authorization;
using eCommerceApp.Application.Consts;

namespace eCommerceApp.Host.Controllers
{
    [Authorize(Roles =Roles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(ProcessCart request)
        {
            var result = await _cartService.AddToCart(request , User.GetUserId()!);
            return result.Success ? Ok(result) : NotFound(result);
        }


        [HttpGet("items")]
        public async Task<IActionResult> Get()
        {
            var result = await _cartService.GetCartItems( User.GetUserId()!);
            return result.Any() ? Ok(result) : NotFound(result);
        }


        [HttpPut("items/update")]
        public async Task<IActionResult> Update(ProcessCart request)
        {
            var result = await _cartService.UpdateQuantity(request, User.GetUserId()!);
            return result.Success ? Ok(result) : NotFound(result);
        }


        [HttpDelete("items/{CartItemId}")]
        public async Task<IActionResult> Delete(Guid CartItemId)
        {
            var result = await _cartService.RemoveFromCart(CartItemId);
            return result.Success ? NoContent() : NotFound(result);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> Clear( )
        {
            var result = await _cartService.Clear(User.GetUserId()!);
            return result.Success ? NoContent() : NotFound(result);
        }


    }
}
