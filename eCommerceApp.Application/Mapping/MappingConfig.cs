
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using Mapster;


namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, GetProduct>()
                 .Map(dest => dest.Base64Image, src => src.Image);
            config.NewConfig<CreateProduct, Product>()
               .Map(dest => dest.Image, src => src.Base64Image);
        }
    }
}
