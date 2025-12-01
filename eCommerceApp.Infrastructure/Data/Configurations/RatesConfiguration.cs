using eCommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Infrastructure.Data.Configurations
{
    public class RatesConfiguration : IEntityTypeConfiguration<Rates>
    {
        public void Configure(EntityTypeBuilder<Rates> builder)
        {
            builder.HasIndex(r => new { r.UserId, r.ProductId }).IsUnique();
        }
    }
}
