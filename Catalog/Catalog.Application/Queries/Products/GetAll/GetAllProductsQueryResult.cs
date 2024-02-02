using Catalog.Application.Queries.Products.Common;

namespace Catalog.Application.Queries.Products.GetAll;

public class GetAllProductsQueryResult
{
    public GetProductQueryResult[]? Products { get; set; }
}