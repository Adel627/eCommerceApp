using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment> , ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository( AppDbContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> GetByUserId(string userId) =>
           await _context.Comments.Where(r => r.UserId == userId).ToListAsync();

        public async Task<IEnumerable<Comment>> GetByProductId(Guid productId) =>
            await _context.Comments.Where(r => r.ProductId == productId).ToListAsync();

    }
}
