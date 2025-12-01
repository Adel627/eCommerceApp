using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Product
{
    public class GetProductDetails 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
      
        public DateTime CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedById { get; set; } = default!;
        
    }
}
