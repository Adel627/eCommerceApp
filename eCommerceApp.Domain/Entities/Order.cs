using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCommerceApp.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public AppUser User { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public ICollection<Payment> Payments { get; set; } = default!;    

    }


}
