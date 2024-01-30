using Catalog.Domain.Abstractions;
using Catalog.Domain.Categories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Products;

public class Product : IEntity<string>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public string Owner { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public Category Category { get; set; } = null!;
}