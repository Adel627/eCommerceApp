using eCommerceApp.Application.Consts;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Host.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace eCommerceApp.Host.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController(ICheckoutService checkoutService , IConfiguration configuration) : ControllerBase
    {
        private readonly ICheckoutService _checkoutService = checkoutService;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("order")]
        public async Task<IActionResult> MakeOrder()
        {

            var result = await _checkoutService.AddOrderAsync(User.GetUserId()!);
            return result.Success ? Ok(result.Value) : NotFound(result);
        }


        [HttpPost("pay/{orderId}")]
        public async Task<IActionResult> Pay(Guid orderId)
        {

            var result = await _checkoutService.CreateCheckoutAsync(orderId);
            return result.Success ? Ok(result.Value) : NotFound(result);
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"].FirstOrDefault();
            var webhookSecret = _configuration["Stripe:WebhookSecret"]; 

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(json, signature, webhookSecret);
            }
            catch (Exception)
            {
                return BadRequest(); 
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                
            }
            else if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var pi = stripeEvent.Data.Object as PaymentIntent;
                await _checkoutService.ConfirmPayWebhook(pi!.Id);
            }
            else if (stripeEvent.Type == "payment_intent.payment_failed")
            {
                var pi = stripeEvent.Data.Object as PaymentIntent;
                await _checkoutService.UpdatePayFailed(pi!.Id);
            }
            return Ok();
        }


        //[HttpPost("confirm-pay/{sessionId}")]
        //public async Task<IActionResult> Confirm(string sessionId)
        //{

        //    var result = await _checkoutService.ConfirmPay(sessionId);
        //    return result.Success ? Ok(result) : NotFound(result);
        //}

    }
}
