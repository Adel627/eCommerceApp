using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApp.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LinePrice => UnitPrice * Quantity;
        
    }

}
