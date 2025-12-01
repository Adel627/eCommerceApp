using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDetails>> GetAllAsync();
        Task<GetCategoryDetails> GetByIdAsync(Guid id);
        Task<GetCategory> GetCurrentByIdAsync(Guid id);
        Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<GetCategory>> GetCurrentAsync();

        Task<ServiceResponse> AddAsync(CreateCategory Category , string? UserId =null);
        Task<ServiceResponse> UpdateAsync(UpdateCategory Category);
        Task<ServiceResponse> DeleteAsync(Guid id);

    }
}
