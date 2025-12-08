

using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task<Order?> GetOrderWithItems(Guid orderId) =>
            await _context.Orders
            .Include(o => o.orderItems).ThenInclude( i => i.Product)
            .ThenInclude(p => p.Images)
            .SingleOrDefaultAsync(o => o.Id == orderId);
    }
}
