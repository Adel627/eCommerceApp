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
        public ICollection<GetProduct>? products { get; set; } = [];
    }
}
