using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Checkout;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Enums;
using eCommerceApp.Domain.Interfaces;
using MapsterMapper;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CheckoutService(ICartRepository cartRepository , IOrderRepository orderRepository
        , IStripePaymentService stripePaymentService , IPaymentRepository paymentRepository , IMapper mapper) : ICheckoutService
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IStripePaymentService _stripePaymentService = stripePaymentService;
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse<GetOrder>> AddOrderAsync(string UserId)
        {
             var cart = await _cartRepository.GetCartItems(UserId);

            if(cart == null || cart.CartItems.Count == 0 ) 
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
                if(item.Quantity > item.Product.Quantity)
                    return
                        new ServiceResponse<GetOrder>(false, $"Our stock can not cover you quantity of {item.Product.Name}");


                order.orderItems.Add(new OrderItem()
                {
                    OrderId = order.Id,
                   ProductId = item.ProductId,
                   UnitPrice = item.Product.Price,
                   Quantity = item.Quantity,
                });
            }

            await _orderRepository.AddAsync(order);

            await _cartRepository.Clear(cart.Id);

            var orderResponse = _mapper.Map<GetOrder>(order);
            return
                 new ServiceResponse<GetOrder>(true, Value: orderResponse);



        }
        
        public async Task<ServiceResponse<CheckoutResponse>> CreateCheckoutAsync(Guid orderId )
        {
            var order = await _orderRepository.GetOrderWithItems(orderId);
            if (order == null)
                return 
                    new ServiceResponse<CheckoutResponse>(false, "There are no orders in the given Id");

            var checkoutResponse = await _stripePaymentService.CreateCheckoutSessionAsync(order);
         
            if (checkoutResponse == null)
                return 
                    new ServiceResponse<CheckoutResponse>(false, "An error occure while create the session");

            return new ServiceResponse<CheckoutResponse>(true,Value: checkoutResponse);

        }

        public async Task ConfirmPayWebhook(string paymentIntendId)
        {

            var payment = await _paymentRepository.GetPaymentWithOrder(paymentIntendId);

            payment!.Status = PaymentStatus.Paid;
            payment.PaymentIntentId = paymentIntendId;

            payment.Order.OrderStatus = OrderStatus.Paid;

            await _paymentRepository.UpdateAsync(payment);

        }
        public async Task UpdatePayFailed(string paymentIntendId)
        {

            var payment = await _paymentRepository.GetPaymentWithOrder(paymentIntendId);

            payment!.Status = PaymentStatus.Failed;

            await _paymentRepository.UpdateAsync(payment);
            
        }

        //public async Task<ServiceResponse> ConfirmPay(string sessionId)
        //{
        //    var paymentStatus = await _stripePaymentService.GetPaymentStatus(sessionId);

        //    if (paymentStatus == PaymentStatus.NotFound)
        //        return new ServiceResponse(false, "There are no sessions with this Id");

        //    var payment = await _paymentRepository.GetPaymentWithOrder(sessionId);

        //    payment!.Status = paymentStatus;

        //    if (paymentStatus == PaymentStatus.Failed)
        //    {
        //        await _paymentRepository.UpdateAsync(payment);
        //        return new ServiceResponse(false, "Failed to confirm this session");
        //    }
        //    payment.Order.OrderStatus = OrderStatus.Paid;

        //    await _paymentRepository.UpdateAsync(payment);

        //    return new ServiceResponse(true, "Payment Confirmed");
        //}
    }
}
