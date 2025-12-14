using eCommerceApp.Application.DTOs.Checkout;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Enums;
using eCommerceApp.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace eCommerceApp.Infrastructure.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public StripePaymentService(IConfiguration configuration,AppDbContext context )
        {
            _configuration = configuration;
            _context = context;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<CheckoutResponse> CreateCheckoutSessionAsync(Order order)
        {
            try
            {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var item in order.orderItems)
                {
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                                
                            },
                            UnitAmount = (long)(item.Product.Price * 100),

                        },
                        Quantity = item.Quantity,
                        TaxRates = new List<string> { "txr_1Sbk3gIX1Nnj8FKTlfK6pi6J" }
                    });
                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["card"],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = $"{_configuration["ReturnUrl"]}/payment-success?orderId={order.Id}&sessionId={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{_configuration["ReturnUrl"]}/payment-cancel?orderId={order.Id}"
                
                };
                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                var payment = new Payment()
                {
                    OrderId = order.Id,
                    Amount = order.TotalAmount,
                    SessionId = session.Id,
                };

                await _context.AddAsync(payment);
                await _context.SaveChangesAsync();

                return new CheckoutResponse()
                {
                    SessionId = session.Id ,
                    PaymentUrl = session.Url
                };
            }
            catch (Exception ) 
            {
              return null!;
            }
        }

        public async Task<PaymentStatus> GetPaymentStatus(string sessionId)
        {
            var SessionService = new SessionService();
            var session = await SessionService.GetAsync(sessionId);

            if (session == null)
                return PaymentStatus.NotFound;
            
            if (session.PaymentStatus == "paid")
                return PaymentStatus.Paid;

            return PaymentStatus.Failed;
        }

      

    }
}
