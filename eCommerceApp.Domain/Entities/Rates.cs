using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class Rates
    {
        public Guid Id { get; set; }
        public Product Product { get; set; } = default!;
        public Guid ProductId { get; set; } 

        public AppUser User { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public Rate Rate { get; set; } = default!;
        public ICollection<Comment> comments { get; set; } = default!;
        
    }
}
