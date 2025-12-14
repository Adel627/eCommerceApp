using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Helpers;
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


        public IQueryable GetAllCurrentAsync(RequestFilters filters)
        {

            var query = _context.Products.Where(p => p.IsDeleted == false);

            if (!string.IsNullOrEmpty(filters.SearchValue))
            {
                query = query.Where(x => x.Name.Contains(filters.SearchValue));
            }

            if (filters.CategoryId.HasValue)
                query = query.Where(m => m.Categories.Any(c => c.CategoryId == filters.CategoryId));

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                query = filters.SortColumn switch
                {
                    "PriceAce" => query.OrderBy(m => m.Price),
                    "PriceDce" => query.OrderByDescending(m => m.Price),
                    _ => query.OrderBy(m => m.Name),
                };
            }
            var source = query.Include(p => p.Categories.Where(c => c.Category.IsDeleted == false))
                                          .ThenInclude(c => c.Category)
                                           .Include(p => p.Images);
                                           
              
            return source;
        }

        public async Task<Product?> GetCurrentByIdAsync(Guid id)
        {
            var result = await _context.Products.Include(p => p.Categories)
                                          .ThenInclude(c => c.Category)
                                          .Include(p => p.Images)
                                          .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            return result;
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
