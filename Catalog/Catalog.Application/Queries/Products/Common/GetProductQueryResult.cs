using Catalog.Application.Queries.Categories.Common;

namespace Catalog.Application.Queries.Products.Common;

public class GetProductQueryResult
{
    public required string Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Owner { get; set; }
    
    public required  string Description { get; set; }
    
    public required decimal Price { get; set; }
    
    public required GetCategoryQueryResult Category { get; set; }
}