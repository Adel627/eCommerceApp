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
        //Task<IEnumerable<GetProduct>> GetCategoryWithProductsAsync(Guid categoryId);
        Task<IEnumerable<GetCategory>> GetCurrentAsync();

        Task<ServiceResponse> AddAsync(CreateCategory Category);
        Task<ServiceResponse> UpdateAsync(UpdateCategory Category);
        Task<ServiceResponse> DeleteAsync(Guid id);

    }
}
