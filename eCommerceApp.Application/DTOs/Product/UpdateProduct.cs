using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product
{
    public class UpdateProduct:CreateProduct
    {
        [Required]
        public Guid Id { get; set; }
    }
}
