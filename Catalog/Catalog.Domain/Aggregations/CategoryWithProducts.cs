namespace Catalog.Domain.Aggregations;

public class CategoryWithProducts
{
    public required string Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Owner { get; set; }
    
    public required string Description { get; set; }
    
    public required List<ProductResult> Products { get; set; }
}