using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Domain.Interfaces
{
    public interface ICommentRepository : IGeneric<Comment> 
    {
        Task<IEnumerable<Comment>> GetByUserId(string userId);

    }
}
