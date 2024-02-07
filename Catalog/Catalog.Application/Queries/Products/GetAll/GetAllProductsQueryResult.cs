using Catalog.Domain.Aggregations;

namespace Catalog.Application.Queries.Products.GetAll;

public class GetAllProductsQueryResult
{
    public ProductWithCategoryResult[]? Products { get; set; }
}