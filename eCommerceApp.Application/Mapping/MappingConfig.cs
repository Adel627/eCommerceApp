
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Checkout;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Helpers;
using Mapster;


namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Product
            config.NewConfig<CreateProduct, Product>()
                .Ignore(dest => dest.Categories)
                .Ignore(dest => dest.Images);

            config.NewConfig<UpdateProduct, Product>()
               .Ignore(dest => dest.Categories)
               .Ignore(dest => dest.Images);

            config.NewConfig<Product, GetProduct>()
                .Map(dest => dest.Images, src => src.Images.Select(i => i.Image))
                  .Map(dest => dest.CategoryNames,
                src => src.Categories.Select(c => c.Category.Name));

            config.NewConfig<Product, GetProductDetails>()
               .Map(dest => dest.Images, src => src.Images.Select(i => i.Image))
                 .Map(dest => dest.CategoryIds,
               src => src.Categories.Select(c => c.CategoryId));



            //Checkout

            config.NewConfig<Order, GetOrder>()
                .Map(dest => dest.orderItems, src => src.orderItems);
        }
    }
}
