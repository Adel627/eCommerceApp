using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;

        [HttpGet("payment-methods")]
        public async Task<IActionResult> GetPaymentMethods()
        {
            var result = await _paymentMethodService.GetPaymentMethodsAsync();
            return result.Any() ? Ok(result) : NotFound(result);
        }
    }
}
