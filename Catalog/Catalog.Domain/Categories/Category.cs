using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Categories;

public class Category : IEntity<string>
{
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public string Owner { get; set; } = null!;
    
    public string Description { get; set; } = null!;
}