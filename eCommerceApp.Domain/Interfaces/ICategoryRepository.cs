using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface ICategoryRepository : IGeneric<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Category>> GetCurrentAsync();
        Task<Category?> GetCurrentByIdAsync(Guid id);

        Task<int> ToggleDelete(Guid id);

    }
}
