

using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
