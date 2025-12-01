using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class CartItems
    {
        public Guid Id { get; set; }
        public Cart Cart { get; set; } = default!;
        public Guid CartId { get; set; }
        public Product Product { get; set; } = default!;
        public Guid ProductId { get; set; } 
        public int Quantity { get; set; }
        public DateTime AddedToCartDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdate {  get; set; } 
        

    }
}
