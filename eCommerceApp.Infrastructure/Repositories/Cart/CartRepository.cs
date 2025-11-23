using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories.Cart
{
    public class CartRepository(AppDbContext context) : ICart
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Achieve>> GetAllCheckoutHistory()
        {
            return await _context.CheckoutAchieves.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)
        {
             _context.CheckoutAchieves.AddRange(checkouts);
            return await _context.SaveChangesAsync();
        }
    }
}
