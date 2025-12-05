using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<ServiceResponse> AddToCart(ProcessCart request , string UserId);
        Task<IEnumerable<GetCartItems>> GetCartItems(string UserId);
        Task<ServiceResponse> RemoveFromCart(Guid CartItemId);
        Task<ServiceResponse> UpdateQuantity(ProcessCart request , string userId);
        Task<ServiceResponse> Clear(string UserId);
    }
}
