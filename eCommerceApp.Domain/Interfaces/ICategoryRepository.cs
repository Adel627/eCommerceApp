using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
    }
}
