using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDetails>> GetAllAsync();
        Task<IEnumerable<GetProduct>> GetAllCurrentAsync();
        Task<GetProductDetails> GetByIdAsync(Guid id);
        Task<GetProduct> GetCurrentByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateProduct product , string? UserId);
      //  Task<ServiceResponse> UpdateAsync(UpdateProduct product);
        Task<ServiceResponse> DeleteAsync(Guid id);

    }
}
