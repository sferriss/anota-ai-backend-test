namespace Catalog.Application.Queries.Categories.Common;

public class GetCategoryQueryResult
{
    public required string Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Owner { get; set; }
    
    public required string Description { get; set; }
}