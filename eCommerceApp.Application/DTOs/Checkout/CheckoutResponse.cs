using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Checkout
{
    public class CheckoutResponse
    {
        public string PaymentUrl { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
    }
}
