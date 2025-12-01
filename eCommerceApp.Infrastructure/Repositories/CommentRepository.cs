using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
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
    }
}
