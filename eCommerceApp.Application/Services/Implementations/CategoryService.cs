using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Helpers;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using MapsterMapper;

namespace eCommerceApp.Application.Services.Implementations
{
    public class CategoryService(ICategoryRepository categoryRepository , IMapper mapper , IImageService imageService) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository= categoryRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IImageService _imageService = imageService;

        public async Task<ServiceResponse> AddAsync(CreateCategory Category , string? UserId)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(Category.Image.FileName)}";
            var imageUploadedResult = await _imageService.UploadImage(Category.Image, imageName, "Images/Category");

            if (!imageUploadedResult.upload)
                return new ServiceResponse(false, imageUploadedResult.errorMessage!);

            var mappedData = _mapper.Map<Category>(Category);
            mappedData.Image = $"Images/Category/{imageName}";
            mappedData.CreatedById = UserId;

            int result = await _categoryRepository.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category added!")
                : new ServiceResponse(false, "Category failed to be added!");

        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _categoryRepository.ToggleDelete(id);
            return result > 0 ? new ServiceResponse(true, "Category deleted!")
                : new ServiceResponse(false, "Category failed to be deleted!");
        }

        public async Task<IEnumerable<GetCategoryDetails>> GetAllAsync()
        {
            var Categories = await _categoryRepository.GetAllAsync();
            if (!Categories.Any())
                return [];
            var mappedData = _mapper.Map<IEnumerable<GetCategoryDetails>>(Categories);
            return mappedData;

        }
        public async Task<IEnumerable<GetCategory>> GetCurrentAsync()
        {
            var Categories = await _categoryRepository.GetCurrentAsync();
            if (!Categories.Any())
                return [];
            var mappedData = _mapper.Map<IEnumerable<GetCategory>>(Categories);
            return mappedData;

        }
        public async Task<GetCategoryDetails> GetByIdAsync(Guid id)
        {
            var Category = await _categoryRepository.GetByIdAsync(id);
            if (Category is null)
                return new GetCategoryDetails();
            return _mapper.Map<GetCategoryDetails>(Category);
        }
        public async Task<GetCategory> GetCurrentByIdAsync(Guid id)
        {
            var Category = await _categoryRepository.GetCurrentByIdAsync(id);
            if (Category is null)
                return new GetCategory();
            return _mapper.Map<GetCategory>(Category);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products =
                await _categoryRepository.GetProductsByCategoryAsync(categoryId);

            if (!products.Any()) return [];

            return _mapper.Map<IEnumerable<GetProduct>>(products);

        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory CategoryDto)
        {
             var currentCategory = await _categoryRepository.GetByIdAsync(CategoryDto.Id);
            if (currentCategory is null)
                return new ServiceResponse(false, "There are no category with the given id");

            //delete image and save new image
             _imageService.Delete(currentCategory.Image);

            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(CategoryDto.Image.FileName)}";
            var imageUploadedResult = await _imageService.UploadImage(CategoryDto.Image, imageName, "Images/Category");

            if (!imageUploadedResult.upload)
                return new ServiceResponse(false, imageUploadedResult.errorMessage!);

            _mapper.Map(CategoryDto , currentCategory);
            currentCategory.Image = $"Images/Category/{imageName}";
            currentCategory.UpdatedDate = DateTime.UtcNow;

            int result = await _categoryRepository.UpdateAsync(currentCategory);
            return result > 0 ? new ServiceResponse(true, "Category Updated!")
               : new ServiceResponse(false, "Category failed to be Updated!");
        }

    
    }
}
