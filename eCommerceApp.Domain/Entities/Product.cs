using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        [Column(TypeName ="decimal(18,2)")]
        public decimal Price {  get; set; }
        public int Quantity {  get; set; }
        public ICollection<ProductImage> Images { get; set; } = default!;
        public ICollection<ProductCategories> Categories { get; set; } = default!;
        public ICollection<Rates> Rates { get; set; }=default!;
    }
}
