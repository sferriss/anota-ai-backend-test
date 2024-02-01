using Catalog.Application.Queries.Categories.Common;

namespace Catalog.Application.Queries.Categories.GetAll;

public class GetAllCategoriesQueryResult
{
    public GetCategoryQueryResult[]? Categories { get; set; }
}