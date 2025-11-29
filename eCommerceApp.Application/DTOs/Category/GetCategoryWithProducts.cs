using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Category
{
    public class GetCategoryWithProducts : CategoryBase
    {
        public Guid Id { get; set; }
        public string Image { get; set; } = default!;
        public ICollection<ProductCategories> products { get; set; } = default!;
    }
}
