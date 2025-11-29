using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class ProductCategories
    {
        public Product Product { get; set; } = default!;
        public Guid ProductId { get; set; } = default;
        public Category Category { get; set; } = default!;
        public Guid CategoryId { get; set; }

    }
}
