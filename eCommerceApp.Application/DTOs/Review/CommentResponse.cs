using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Review
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public Guid ProductId { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
    }
}
