using System.Text.Json.Serialization;

namespace Catalog.Domain.Files;

public class ItemDto
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    
    [JsonPropertyName("price")]
    public required decimal Price { get; set; }
}