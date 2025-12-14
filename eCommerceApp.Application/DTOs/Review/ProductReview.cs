using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Review
{
    public class ProductReview
    {
        public IEnumerable<RateResponse> RateResponse { get; set; } = new List<RateResponse>();
        public IEnumerable<CommentResponse> CommentResponse { get; set; } = new List<CommentResponse>();
   
    }
}
