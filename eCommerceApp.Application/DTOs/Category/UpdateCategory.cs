using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Category
{
    public class UpdateCategory:CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public IFormFile Image { get; set; } = default!;

    }

}
