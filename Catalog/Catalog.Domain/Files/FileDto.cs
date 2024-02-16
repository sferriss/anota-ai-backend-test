using System.Text.Json.Serialization;

namespace Catalog.Domain.Files;

public class FileDto
{
    [JsonPropertyName("owner")]
    public required string Owner { get; set; }

    [JsonPropertyName("catalog")]
    public required CatalogDto Catalog { get; set; }
}