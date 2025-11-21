using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories.Cart
{
    public class PaymentMethodRepository(AppDbContext context) : IPaymentMethod
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync() =>
          await  _context.PaymentMethods.AsNoTracking().ToListAsync();
        
    }
}
