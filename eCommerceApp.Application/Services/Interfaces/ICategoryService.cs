using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId);

        Task<ServiceResponse> AddAsync(CreateCategory Category);
        Task<ServiceResponse> UpdateAsync(UpdateCategory Category);
        Task<ServiceResponse> DeleteAsync(Guid id);

    }
}
