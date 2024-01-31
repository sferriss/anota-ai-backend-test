using Catalog.Domain.Abstractions;
using Catalog.Domain.Categories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Products;

public class Product : IEntity<string>
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    [BsonElement("title")]
    public string Title { get; set; } = null!;
    
    [BsonElement("owner")]
    public string Owner { get; set; } = null!;
    
    [BsonElement("description")]
    public string Description { get; set; } = null!;
    
    [BsonElement("price")]
    public decimal Price { get; set; }
    
    [BsonElement("category")]
    public Category Category { get; set; } = null!;
}