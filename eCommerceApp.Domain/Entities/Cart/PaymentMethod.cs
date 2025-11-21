using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Entities.Cart
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=string.Empty;
    }
}
