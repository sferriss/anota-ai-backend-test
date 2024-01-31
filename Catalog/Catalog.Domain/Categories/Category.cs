using Catalog.Domain.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Categories;

public class Category : IEntity<string>
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
}