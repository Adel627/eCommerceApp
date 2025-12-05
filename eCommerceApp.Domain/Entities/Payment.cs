using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApp.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
  
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public PaymentStatus Status { get; set; }
        public string? SessionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }


}
