
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Helpers;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Implementations
{
    public class ProductService(IProductRepository productRepository ,IImageService imageService, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IImageService _imageService = imageService;
        private readonly IMapper _mapper = mapper;

        public  async Task<ServiceResponse> AddAsync(CreateProduct product , string? UserId)
        {
            var mappedData = _mapper.Map<Product>(product);

            if (product.Categories?.Count > 0)
            {
                mappedData.Categories = new List<ProductCategories>();
                foreach (var category in product.Categories)
                    mappedData.Categories.Add(new ProductCategories() { CategoryId = category });
            }

            mappedData.Images = new List<ProductImage>();
            foreach (var image in product.Images)
            {
                var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var imageUploadedResult = await _imageService.UploadImage(image, imageName, "Images/Product");

                if (!imageUploadedResult.upload)
                    return new ServiceResponse(false, imageUploadedResult.errorMessage!);

                mappedData.Images.Add(new ProductImage() { Image = $"Images/Product/{imageName}" });
            }

          

            mappedData.CreatedById = UserId;

            int result = await _productRepository.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product added!")
                : new ServiceResponse(false, "Product failed to be added!");

        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _productRepository.ToggleDelete(id);
            return result > 0 ? new ServiceResponse(true, "Product deleted change!")
                : new ServiceResponse(false, "Product failed to be deleted change!");
        }

        public async Task<IEnumerable<GetProductDetails>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (!products.Any())
                return [];
            var mappedData = _mapper.Map<IEnumerable<GetProductDetails>>(products);
            return mappedData;

        }
        public async Task<IEnumerable<GetProduct>> GetAllCurrentAsync()
        {
            var products = await _productRepository.GetAllCurrentAsync();
            if (!products.Any())
                return [];

            var mappedData = _mapper.Map<IEnumerable<GetProduct>>(products);
            
            return mappedData;
        }
        public async Task<GetProductDetails> GetByIdAsync(Guid id)
        {
           var product = await _productRepository.GetByIdAsync(id);
            if(product is null)
                return new GetProductDetails();
            return _mapper.Map<GetProductDetails>(product);
        }

        public async Task<GetProduct> GetCurrentByIdAsync(Guid id)
        {
            var Category = await _productRepository.GetCurrentByIdAsync(id);
            if (Category is null)
                return new GetProduct();
            return _mapper.Map<GetProduct>(Category);
        }

        //public async Task<ServiceResponse> UpdateAsync(UpdateProduct model) 
        //{
        //    var product = await _productRepository.GetCurrentByIdAsync(model.Id); 
        //    if(product is null)
        //        new ServiceResponse(false, "There are no product with the given Id");

        //    foreach (var image in product!.Images)
        //        _imageService.Delete(image.Image); ;

        //    if (model.Categories?.Count > 0)
        //    {
        //        product!.Categories = new List<ProductCategories>();
        //        foreach (var category in product.Categories)
        //            product.Categories.Add(new ProductCategories() { CategoryId = category.CategoryId });
        //    }

            
            
        //    product!.Images = new List<ProductImage>();
        //    foreach (var image in product.Images)
        //    {
        //        var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        //        var imageUploadedResult = await _imageService.UploadImage(image, imageName, "Images/Product");

        //        if (!imageUploadedResult.upload)
        //            return new ServiceResponse(false, imageUploadedResult.errorMessage!);

        //        mappedData.Images.Add(new ProductImage() { Image = $"Images/Product/{imageName}" });
        //    }

        //    int result = await _productRepository.UpdateAsync(mappedData);
        //    return result > 0 ? new ServiceResponse(true, "Product Updated!")
        //       : new ServiceResponse(false, "Product failed to be Updated!");
        //}
    
    }
}
