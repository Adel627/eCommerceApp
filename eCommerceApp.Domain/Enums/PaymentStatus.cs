using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 0,
        Paid = 1,
        Failed = 2,
        NotFound = 3
    }
}
