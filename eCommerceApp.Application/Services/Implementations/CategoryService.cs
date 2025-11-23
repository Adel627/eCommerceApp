
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> categoryInterface, IMapper mapper
           , ICategoryRepository categorySpecifies) : ICategoryService
    {

        private readonly IGeneric<Category> _categoryInterface = categoryInterface;
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categorySpecifies = categorySpecifies;


        public async Task<ServiceResponse> AddAsync(CreateCategory Category)
        {
            var mappedData = _mapper.Map<Category>(Category);
            int result = await _categoryInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category added!")
                : new ServiceResponse(false, "Category failed to be added!");

        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _categoryInterface.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Category deleted!")
                : new ServiceResponse(false, "Category failed to be deleted!");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var Categories = await _categoryInterface.GetAllAsync();
            if (!Categories.Any())
                return [];
            var mappedData = _mapper.Map<IEnumerable<GetCategory>>(Categories);
            return mappedData;

        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var Category = await _categoryInterface.GetByIdAsync(id);
            if (Category is null)
                return new GetCategory();
            return _mapper.Map<GetCategory>(Category);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products =
                await _categorySpecifies.GetProductsByCategoryAsync(categoryId);

            if (!products.Any())  return [];

            return _mapper.Map<IEnumerable<GetProduct>>(products);
               
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory Category)
        {
            var mappedData = _mapper.Map<Category>(Category);
            int result = await _categoryInterface.UpdateAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category Updated!")
               : new ServiceResponse(false, "Category failed to be Updated!");
        }

    
    }
}
