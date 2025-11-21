using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface IPaymentService
    {
        Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts,
          IEnumerable< ProcessCart> carts);
    }
}
