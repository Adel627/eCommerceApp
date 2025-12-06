using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCommerceApp.Application.DTOs.Checkout
{
    public class GetOrderItems
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LinePrice {  get; set; }
    }
}
