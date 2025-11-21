using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IPaymentMethod paymentMethod , IMapper mapper) : IPaymentMethodService
    {
        private readonly IPaymentMethod _paymentMethod = paymentMethod;
        private readonly IMapper _mapper = mapper;

       public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync()
       {
            var paymentMethods = await _paymentMethod.GetPaymentMethodsAsync();
            if (!paymentMethods.Any()) return [];
            var mappedData = _mapper.Map<IEnumerable<GetPaymentMethod>>(paymentMethods);
            return mappedData;
       }
    }
}
