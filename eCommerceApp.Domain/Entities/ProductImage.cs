using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class ProductImage
    {
        public Guid ProductId {  get; set; }
        public Product Product { get; set; } = default!;
        public string Image { get; set; } = default!;
    }
}
