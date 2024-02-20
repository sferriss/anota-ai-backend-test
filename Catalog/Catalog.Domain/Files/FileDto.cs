using System.Text.Json.Serialization;

namespace Catalog.Domain.Files;

public class FileDto
{
    public FileDto(string owner, CatalogDto[] catalog)
    {
        Owner = owner;
        Catalog = catalog;
    }

    [JsonPropertyName("owner")]
    public string Owner { get; set; }

    [JsonPropertyName("catalog")]
    public CatalogDto[] Catalog { get; set; }
}