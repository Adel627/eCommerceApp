using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class Rates
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public Product Product { get; set; } = default!;
        public Guid ProductId { get; set; } 
        public int Rate { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        

    }
}
