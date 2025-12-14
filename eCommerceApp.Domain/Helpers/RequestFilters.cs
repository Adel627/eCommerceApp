namespace eCommerceApp.Domain.Helpers
{
    public class RequestFilters
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SearchValue { get; init; }
        public Guid? CategoryId { get; init; }
        public string? SortColumn { get; init; }
    }
}
