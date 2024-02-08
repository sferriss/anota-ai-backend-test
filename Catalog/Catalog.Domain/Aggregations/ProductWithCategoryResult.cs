namespace Catalog.Domain.Aggregations;

public class ProductWithCategoryResult
{
    public required string Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Owner { get; set; }
    
    public required  string Description { get; set; }
    
    public required decimal Price { get; set; }

    public CategoryResult? Category { get; set; }
}