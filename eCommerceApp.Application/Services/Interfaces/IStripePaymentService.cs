using eCommerceApp.Application.DTOs.Checkout;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface IStripePaymentService
    {
        Task<CheckoutResponse> CreateCheckoutSessionAsync(Order order);
        Task<PaymentStatus> GetPaymentStatus(string sessionId);

    }
}
