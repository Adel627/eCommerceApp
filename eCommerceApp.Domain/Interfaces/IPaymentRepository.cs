using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface IPaymentRepository : IGeneric<Payment>
    {
        Task<Payment?> GetPaymentWithOrder(string sessionId);
    }
}
