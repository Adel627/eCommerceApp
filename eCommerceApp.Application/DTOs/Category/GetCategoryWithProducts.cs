using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Category
{
    public class GetCategoryWithProducts 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Image { get; set; } = default!;
        public ICollection<ProductCategories> products { get; set; } = default!;
    }
}
