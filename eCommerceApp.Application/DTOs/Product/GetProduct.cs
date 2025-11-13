using eCommerceApp.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Product
{
    public class GetProduct:ProductBase
    {
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; }

    }
}
