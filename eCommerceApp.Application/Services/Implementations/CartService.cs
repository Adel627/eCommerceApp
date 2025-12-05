using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Authentication;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productInterface
          ,ICartIemsRepository cartIemsRepository) : ICartService
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IProductRepository _productInterface = productInterface;
        private readonly ICartIemsRepository _cartIemsRepository = cartIemsRepository;

        public async Task<ServiceResponse> AddToCart(ProcessCart request , string UserId)
        {
            var product = await _productInterface.GetCurrentByIdAsync(request.ProductId);
            if (product == null)
                return new ServiceResponse(false, "there are no products with the given Id");

            if (request.Quantity > product.Quantity)
                return new ServiceResponse(false, "the quantity is grather than our stock");

            var cart = await _cartRepository
                .GetCartWithSpecificItem(UserId, request.ProductId); 

            if(cart == null)
            {
                cart = new Cart() { UserId = UserId };
                await _cartRepository.AddAsync(cart);
            }

            if(cart.CartItems.Count > 0 )
            {
                cart.CartItems.FirstOrDefault()!.Quantity += request.Quantity;
                cart.CartItems.FirstOrDefault()!.LastUpdate = DateTime.UtcNow;
  
                cart.LastUpdate = DateTime.UtcNow;
                await _cartRepository.UpdateAsync(cart);
                return new ServiceResponse(true, "Product added");
            }

            var item= new CartItems() 
            {   CartId = cart.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _cartIemsRepository.AddAsync(item);
            cart.LastUpdate = DateTime.UtcNow;
            await _cartRepository.UpdateAsync(cart);

            return new ServiceResponse(true, "Product added to cart!!");
        }
        public async Task<IEnumerable<GetCartItems>> GetCartItems(string UserId)
        {
            var cart= await _cartRepository.GetCartItems(UserId);
            if (cart == null)
            {
                cart = new Cart() { UserId = UserId };
                await _cartRepository.AddAsync(cart);

                return [];
                   
            }
            var mappedData = _mapper.Map<IEnumerable<GetCartItems>>(cart.CartItems);

            return !cart.CartItems.Any()
                ? []
                : mappedData;

        }
        public async Task<ServiceResponse> RemoveFromCart(Guid CartItemId)
        {
            var cartItem = await _cartIemsRepository.GetByIdAsync(CartItemId);
            if (cartItem is null)
                return new ServiceResponse(false, "There are no item with this Id");

            await _cartIemsRepository.DeleteAsync(CartItemId);
            return new ServiceResponse(true, "Item Removed from the cart");

        }

        public async Task<ServiceResponse> UpdateQuantity(ProcessCart request, string UserId)
        {
            var product = await _productInterface.GetCurrentByIdAsync(request.ProductId);
            if (product == null)
                return new ServiceResponse(false, "there are no products with the given Id");

            if (request.Quantity > product.Quantity)
                return new ServiceResponse(false, "the quantity is grather than our stock");
            var cart = await _cartRepository.GetCartWithSpecificItem(UserId , request.ProductId);

            if (cart == null)
            {
                cart = new Cart() { UserId = UserId };
                await _cartRepository.AddAsync(cart);
                return new ServiceResponse(false, "there are no products in the cart");

            }

            if(!cart.CartItems.Any())
                return new ServiceResponse(false, "there are no products with this id in the cart");

            cart.CartItems.FirstOrDefault()!.Quantity = request.Quantity;
            cart.CartItems.FirstOrDefault()!.LastUpdate = DateTime.UtcNow;
            cart.LastUpdate = DateTime.UtcNow;  
              await _cartRepository.UpdateAsync(cart);
                return new ServiceResponse(true, "Item Updated!!");

        }
        public async Task<ServiceResponse> Clear(string UserId)
        {
            var cart = await _cartRepository.GetByUserId(UserId);

            if (cart == null)
            {
                cart = new Cart() { UserId = UserId };
                await _cartRepository.AddAsync(cart);
                return new ServiceResponse(false, "You do not have any products in cart");
            }
           bool result = await _cartRepository.Clear(cart.Id);

            if(!result)
                return new ServiceResponse(false, "You do not have any products in cart");

            return new ServiceResponse(true, "Cart Cleared Successfully");

        }
    }
}
