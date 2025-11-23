using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class CreateAchieve
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string? UserId { get; set; } = string.Empty;
    }
}
