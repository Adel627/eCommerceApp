
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Domain.Interfaces
{
    public interface ICartRepository : IGeneric<Cart>
    {
        Task<Cart?> GetByUserId(string userId);
        Task<Cart?> GetCartItems(string userId);
        Task<Cart?> GetCartWithSpecificItem(string UserId, Guid ProductId);

    }
}
