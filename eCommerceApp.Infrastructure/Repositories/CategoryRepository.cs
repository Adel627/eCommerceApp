using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category> ,ICategoryRepository
    {
        private readonly AppDbContext _context ;
        public CategoryRepository(AppDbContext context):base(context) 
        {
            _context = context;  
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products = await _context.Products
                //.Include(p => p.Category)
               // .Where(p => p.CategoryId == categoryId)
                .AsNoTracking().ToListAsync();
            return products.Any() ? products : [];
        }

        public async Task<IEnumerable<Category>> GetCurrentAsync()
        {
            return await _context.Categories.AsNoTracking()
                .Where(c => c.IsDeleted == false)
                .ToListAsync();
        }
        public async Task<Category?> GetCurrentByIdAsync(Guid id)
        {
            var result = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false );
            return result;
        }
        public async Task<int> ToggleDelete(Guid id)
        {
           var category = await GetByIdAsync(id);
            if(category == null) return 0;
            category.IsDeleted = !category.IsDeleted;
            category.DeletedDate = DateTime.UtcNow;
            return await _context.SaveChangesAsync();
            
        }
    }
}
