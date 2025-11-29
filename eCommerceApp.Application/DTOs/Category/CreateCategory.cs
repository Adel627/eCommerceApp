using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Category
{
    public class CreateCategory:CategoryBase
    {
        public IFormFile Image { get; set; } = default!;
    }

}
