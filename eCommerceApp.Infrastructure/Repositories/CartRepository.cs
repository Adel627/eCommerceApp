using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Cart> , ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context):base(context) 
        {
            _context = context;
            
        }

        public async Task<Cart?> GetByUserId(string userId) =>
            await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

        public async Task<Cart?> GetCartItems(string userId) =>
            await _context.Carts.Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.UserId == userId);

        public async Task<Cart?> GetCartWithSpecificItem(string UserId, Guid ProductId)=>
            await _context.Carts
            .Include(c => c.CartItems.SingleOrDefault(c => c.ProductId == ProductId))
            .SingleOrDefaultAsync(c => c.UserId == UserId);
    }
}
