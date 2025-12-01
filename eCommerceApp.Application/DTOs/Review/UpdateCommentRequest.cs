using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Review
{
    public class UpdateCommentRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; } = default!;
    }
}
