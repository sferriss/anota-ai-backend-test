using Catalog.Domain.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Categories;

public class Category : IEntity<string>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public string Owner { get; set; } = null!;
    
    public string Description { get; set; } = null!;
}