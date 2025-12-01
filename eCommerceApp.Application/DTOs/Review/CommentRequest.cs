using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Review
{
    public class CommentRequest
    {
        [Required]
        public Guid ProductId { get; set; }


        [Required]
        public string Content { get; set; } = default!;
    }
}
