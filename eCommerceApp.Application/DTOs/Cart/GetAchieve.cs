using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class GetAchieve
    {
        public string ProductName { get; set; }=string.Empty;
        public int QuantityOrdered { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public decimal AmountPayed {  get; set; }
        public DateTime DatePurchased {  get; set; }

    }
}
