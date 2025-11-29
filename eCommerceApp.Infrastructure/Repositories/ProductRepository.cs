using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace eCommerceApp.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }

  
        public async Task<IEnumerable<Product>> GetAllNowAsync()
        {
            return await _context.Products.AsNoTracking()
                .Where(c => c.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<int> ToggleDelete(Guid id)
        {
            var product = await GetByIdAsync(id);
            if (product == null) return 0;
            product.IsDeleted = !product.IsDeleted;
            product.DeletedDate = DateTime.UtcNow;
            return await _context.SaveChangesAsync();
        }


    }
}
