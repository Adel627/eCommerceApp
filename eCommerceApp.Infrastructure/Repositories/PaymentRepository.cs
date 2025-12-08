using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment> , IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Payment?> GetPaymentWithOrder(string sessionId) =>
            await _context.Payments
            .Include( p => p.Order)
            .SingleOrDefaultAsync( p => p.SessionId == sessionId );
        
    }
}
