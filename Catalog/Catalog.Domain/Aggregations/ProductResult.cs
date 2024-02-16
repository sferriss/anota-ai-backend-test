namespace Catalog.Domain.Aggregations;

public class ProductResult
{
    public required string Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Owner { get; set; }
    
    public required string Description { get; set; }
    
    public required decimal Price { get; set; }
    
    public required string CategoryId { get; set; }
}