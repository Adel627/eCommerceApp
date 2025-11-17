
using eCommerceApp.Application.DTOs;
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
    public class ProductService(IGeneric<Product> productInterface , IMapper mapper) : IProductService
    {
        private readonly IGeneric<Product> _productInterface = productInterface;
        private readonly IMapper _mapper = mapper;

        public  async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var mappedData = _mapper.Map<Product>(product); 
           int result = await _productInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product added!")
                : new ServiceResponse(false, "Product failed to be added!");

        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _productInterface.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Product deleted!")
                : new ServiceResponse(false, "Product failed to be deleted!");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var products = await _productInterface.GetAllAsync();
            if (!products.Any())
                return [];
            var mappedData = _mapper.Map<IEnumerable<GetProduct>>(products);
            return mappedData;

        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
           var product = await _productInterface.GetByIdAsync(id);
            if(product is null)
                return new GetProduct();
            return _mapper.Map<GetProduct>(product);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product) //There are a problem in update and update of generic repo
        {
            var mappedData = _mapper.Map<Product>(product);
            int result = await _productInterface.UpdateAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product Updated!")
               : new ServiceResponse(false, "Product failed to be Updated!");
        }
    
    }
}
