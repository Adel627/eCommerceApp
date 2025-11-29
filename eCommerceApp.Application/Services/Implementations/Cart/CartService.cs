using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Interfaces.Cart;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class CartService(ICart cart, IMapper mapper, IProductRepository productInterface,
                       IPaymentMethod paymentMethod, IPaymentService paymentService , IUserManagement userManagement) : ICartService
    {
        private readonly ICart _cart = cart;
        private readonly IMapper _mapper = mapper;
        private readonly IProductRepository _productInterface = productInterface;
        private readonly IPaymentMethod _paymentMethod = paymentMethod;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IUserManagement _userManagement = userManagement;

        public async Task<ServiceResponse> Checkout(Checkout checkout)
        {
            var (products, totalAmount) = await GetTotalAmount(checkout.Carts);
            var paymentMethods = await _paymentMethod.GetPaymentMethodsAsync();

            if (checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id)
                return await _paymentService.Pay(totalAmount, products, checkout.Carts);

            return new ServiceResponse(false, "Invalid payment method");
        }

        public async Task<IEnumerable<GetAchieve>> GetAchievesAsync()
        {
            var history = await _cart.GetAllCheckoutHistory();
            if (!history.Any()) return [];

            var groubByCustomerId = history.GroupBy(h => h.UserId).ToList();
             var products = await _productInterface.GetAllAsync();
            var achieves = new List<GetAchieve>();

            foreach (var customerId in groubByCustomerId) 
            {
                var customerDetails =
                     await _userManagement.GetUserById(customerId.Key!);

                foreach(var item in customerId)
                {
                  var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                    achieves.Add(new GetAchieve
                    {
                        CustomerName = customerDetails.FullName,
                        CustomerEmail = customerDetails.Email!,
                        ProductName = product!.Name!,
                        AmountPayed = item.Quantity * product.Price,
                        QuantityOrdered = item.Quantity,
                        DatePurchased = item.CreatedDate

                    });
                }
            
            }
            return achieves;
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
        {
            var mappedData = _mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await _cart.SaveCheckoutHistory(mappedData);
            return result > 0 ?
               new ServiceResponse(true, "Checkout Achieved") :
                new ServiceResponse(false, "Error Occured Saving");
        }

        private async Task<(IEnumerable<Product>, decimal)> GetTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if (!carts.Any()) return ([], 0);

            var products = await _productInterface.GetAllAsync();
            if (!products.Any()) return ([], 0);

            var cartProducts = carts
              .Select(carItem => products.FirstOrDefault(p => p.Id == carItem.ProductId))
              .Where(product => product != null)
              .ToList();

            var productById = cartProducts.ToDictionary(p => p.Id);

            var totalAmount = carts
               .Where(carItem => productById.ContainsKey(carItem.ProductId))
               .Sum(cartItem => cartItem.Quantity * productById[cartItem.ProductId]!.Price);

            return (cartProducts!, totalAmount);


            //var cartProducts = carts
            //    .Select(carItem => products.FirstOrDefault(p => p.Id == carItem.ProductId))
            //    .Where(product => product != null)
            //    .ToList();

            //var totalAmount = carts
            //   .Where(carItem => cartProducts.Any(p => p.Id == carItem.ProductId))
            //   .Sum(cartItem => cartItem.Quantity * cartProducts
            //        .FirstOrDefault(p => p.Id == cartItem.ProductId)!.Price);
            //return (cartProducts! ,  totalAmount);    

        }
    }
}
