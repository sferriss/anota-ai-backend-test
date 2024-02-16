using System.Text.Json.Serialization;

namespace Catalog.Domain.Files;

public class CatalogDto
{
    [JsonPropertyName("category_title")]
    public required string CategoryTitle { get; set; }
    
    [JsonPropertyName("category_description")]
    public required string CategoryDescription { get; set; }

    [JsonPropertyName("items")]
    public required ItemDto[] Items { get; set; }
}