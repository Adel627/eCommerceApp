using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class Comment
    {
        public Rates Rates { get; set; } = default!;
        public Guid RatesId { get; set; }
        public string Content { get; set; } = default!;
    }
}
