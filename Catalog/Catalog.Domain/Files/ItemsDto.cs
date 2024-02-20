using System.Text.Json.Serialization;

namespace Catalog.Domain.Files;

public class ItemsDto
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }
}