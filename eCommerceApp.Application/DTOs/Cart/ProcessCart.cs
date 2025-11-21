using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class ProcessCart
    {
        public Guid ProductId { get; set; }
        public int Quantity {  get; set; }
    }
}
