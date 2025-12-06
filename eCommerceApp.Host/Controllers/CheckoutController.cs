using eCommerceApp.Application.Consts;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Host.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController(ICheckoutService checkoutService) : ControllerBase
    {
        private readonly ICheckoutService _checkoutService = checkoutService;


        [HttpPost("order")]
        public async Task<IActionResult> MakeOrder()
        {
          
            var result = await _checkoutService.AddOrderAsync(User.GetUserId()!);
            return result.Success ? Ok(result.Value) : NotFound(result);
        }
    
    }
}
