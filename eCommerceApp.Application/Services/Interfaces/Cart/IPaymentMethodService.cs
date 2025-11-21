using eCommerceApp.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync();
      
    }
}
