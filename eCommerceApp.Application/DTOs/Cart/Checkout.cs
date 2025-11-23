using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class Checkout
    {
        [Required]
        public Guid PaymentMethodId { get; set; }

        [Required]
        public IEnumerable<ProcessCart> Carts { get; set; } = [];
    }
}
