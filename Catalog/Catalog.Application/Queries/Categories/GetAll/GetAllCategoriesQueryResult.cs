using Catalog.Domain.Aggregations;

namespace Catalog.Application.Queries.Categories.GetAll;

public class GetAllCategoriesQueryResult
{
    public CategoryResult[]? Categories { get; set; }
}