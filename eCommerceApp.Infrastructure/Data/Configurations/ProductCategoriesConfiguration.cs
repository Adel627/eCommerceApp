using eCommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Data.Configurations
{
    public class ProductCategoriesConfiguration : IEntityTypeConfiguration<ProductCategories>
    {
        public void Configure(EntityTypeBuilder<ProductCategories> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });
        }
    }
}
