using eCommerceApp.Domain.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces.Cart
{
    public interface IPaymentMethod
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(); 
    }
}
