using System.Text.Json.Serialization;

namespace Catalog.Domain.Files;

public class CatalogDto
{
    [JsonPropertyName("category_title")]
    public string? CategoryTitle { get; set; }
    
    [JsonPropertyName("category_description")]
    public string? CategoryDescription { get; set; }

    [JsonPropertyName("items")]
    public ItemsDto[]? Items { get; set; }
}