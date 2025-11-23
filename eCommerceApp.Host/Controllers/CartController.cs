using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Infrastructure.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        [Authorize(Roles = Roles.User)]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(Checkout checkout)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartService.Checkout(checkout);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost("save-checkout")]
        public async Task<IActionResult> SaveCheckout(IEnumerable<CreateAchieve> achieves)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartService.SaveCheckoutHistory(achieves);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("get-achieves")]
        public async Task<IActionResult> GetAllCheckoutHistory()
        {
            var result = await _cartService.GetAchievesAsync();
            return result.Any() ? Ok(result) : BadRequest(result);
        }
    }
}
