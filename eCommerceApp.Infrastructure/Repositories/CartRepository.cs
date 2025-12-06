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
            .ThenInclude(c => c.Product)
            .SingleOrDefaultAsync(c => c.UserId == userId);

        public async Task<Cart?> GetCartWithSpecificItem(string UserId, Guid ProductId)=>
            await _context.Carts
            .Include(c => c.CartItems.Where(c => c.ProductId == ProductId))
            .SingleOrDefaultAsync(c => c.UserId == UserId);

        public async Task<bool> Clear(Guid cartId)
        {
            var items = await _context.CartItems
                .Where(c => c.CartId == cartId).ToListAsync();
           
            if(items.Count == 0) return false;

             _context.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
