using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Category
{
    public class CreateCategory
    {
        [Required]
        public string Name { get; set; } = default!;
        public IFormFile Image { get; set; } = default!;
    }

}
