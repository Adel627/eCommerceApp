using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface IRateRepository : IGeneric<Rates>
    {
        Task<Rates?> GetByUserIdandProductId(string userId , Guid productId);
    }
}
