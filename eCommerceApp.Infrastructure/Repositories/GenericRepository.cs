using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IGeneric<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
             var result = await _context.Set<TEntity>().FindAsync(id);
            return result!;
            
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
             _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync();   
        }

        public async Task<int> DeleteAsync(Guid id) 
        {
          var entity = await GetByIdAsync(id);
            if (entity != null) 
            {
                _context.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
