using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class GetCartItems
    {

        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedToCartDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdate { get; set; }

    }
}
