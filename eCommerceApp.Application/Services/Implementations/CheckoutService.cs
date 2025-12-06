using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Checkout;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CheckoutService(ICartRepository cartRepository , IOrderRepository orderRepository
         , IMapper mapper) : ICheckoutService
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse<GetOrder>> AddOrderAsync(string UserId)
        {
             var cart = await _cartRepository.GetCartItems(UserId);

            if(cart == null || cart.CartItems.Count ==01 ) 
                return new ServiceResponse<GetOrder>(false , "There are no products in the cart");

            var subTotal = cart.GetTotal;
            var Tax = (decimal)0.14 * cart.GetTotal;


            var order = new Order()
            {
                SubTotal = subTotal,
                Tax = Tax,
                TotalAmount = subTotal + Tax,
                UserId = UserId
            };
            
            foreach(var item in cart.CartItems)
            {
                order.orderItems.Add(new OrderItem()
                {
                    OrderId = order.Id,
                   ProductId = item.ProductId,
                   UnitPrice = item.Product.Price,
                   Quantity = item.Quantity,
                });
            }

            await _orderRepository.AddAsync(order);
            var orderResponse = _mapper.Map<GetOrder>(order);
            return
                 new ServiceResponse<GetOrder>(true, Value: orderResponse);



        }
    }
}
