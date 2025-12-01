using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepository<CartItems> , ICartIemsRepository
    {
        private readonly AppDbContext _context;
        public CartItemRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
