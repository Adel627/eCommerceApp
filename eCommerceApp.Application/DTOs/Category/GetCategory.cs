using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Category
{
    public class GetCategory:CategoryBase
    {
        public Guid Id { get; set; }
        public string Image { get; set; } = default!;
        public DateTime CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; }

    }
}
