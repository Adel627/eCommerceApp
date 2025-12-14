using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Checkout;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICheckoutService
    {
        
        Task<ServiceResponse<GetOrder>> AddOrderAsync(string UserId);
        Task<ServiceResponse<CheckoutResponse>> CreateCheckoutAsync(Guid orderId);
         Task ConfirmPayWebhook(string paymentIntendId);
        Task UpdatePayFailed(string paymentIntendId);
       // Task<ServiceResponse> ConfirmPay(string sessionId);
    }
}
