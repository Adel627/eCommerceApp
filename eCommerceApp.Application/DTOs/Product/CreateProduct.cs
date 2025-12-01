using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product
{
    public class CreateProduct
    {
     

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public ICollection<IFormFile> Images { get; set; } = new List<IFormFile>();

        public IList<Guid>? Categories { get; set; } = new List<Guid>();
    }
}
