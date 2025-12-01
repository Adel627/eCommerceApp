using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Review
{
    public class RateRequest
    {
     
        public Guid ProductId { get; set; }
        public int Rate { get; set; } = default!;
    }
}
