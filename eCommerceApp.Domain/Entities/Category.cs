namespace eCommerceApp.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Image {  get; set; }= default!;
        public ICollection<ProductCategories> products { get; set; } = default!;
    }
}
