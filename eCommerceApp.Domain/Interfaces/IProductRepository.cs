using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface IProductRepository : IGeneric<Product>
    {
        IQueryable GetAllCurrentAsync(RequestFilters requestFilters);
        Task<int> ToggleDelete(Guid id);
        Task<Product?> GetCurrentByIdAsync(Guid id);
        
    }
}
