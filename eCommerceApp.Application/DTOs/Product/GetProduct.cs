using eCommerceApp.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Product
{
    public class GetProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
   
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public double AverageRating { get; set; }
        public IList<string> CategoryNames {  get; set; }= new List<string>();
        public IList<string> Images {  get; set; }= new List<string>();
        public DateTime CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; }

    }
}
