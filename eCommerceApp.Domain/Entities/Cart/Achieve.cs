using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities.Cart
{
    public class Achieve
    {
        public Guid Id {  get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? UserId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
