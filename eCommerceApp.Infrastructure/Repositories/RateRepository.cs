using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class RateRepository : GenericRepository<Rates> , IRateRepository
    {
        private readonly AppDbContext _context;
        public RateRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rates>> GetByUserId(string userId) =>
            await _context.Rates.Where(r => r.UserId == userId).ToListAsync();
        

        public async Task<Rates?> GetByUserIdandProductId(string userId, Guid productId) =>
            await _context.Rates.SingleOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);


    }
}
