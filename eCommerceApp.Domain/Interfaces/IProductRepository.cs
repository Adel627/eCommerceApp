using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface IProductRepository : IGeneric<Product>
    {
        Task<IEnumerable<Product>> GetAllCurrentAsync();
        Task<int> ToggleDelete(Guid id);
        Task<Product?> GetCurrentByIdAsync(Guid id);
        
    }
}
