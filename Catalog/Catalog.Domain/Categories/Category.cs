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
    public required string Title { get; set; }
    
    [BsonElement("owner")]
    public required string Owner { get; set; }
    
    [BsonElement("description")]
    public required string Description { get; set; }

    public void UpdateCategory(string? title, string? owner, string? description)
    {
        Title = title ?? Title;
        Owner = owner ?? Owner;
        Description = description ?? Description;
    }
}