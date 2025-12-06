using eCommerceApp.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }    
        public string UserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdate { get; set; }
        public ICollection<CartItems> CartItems { get; set; } = default!;
        public decimal GetTotal => CartItems.Sum( c => c.Product.Price * c.Quantity);

    }
}