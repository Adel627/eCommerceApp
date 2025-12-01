using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Category
{
    public class UpdateCategory
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public IFormFile Image { get; set; } = default!;

    }

}
