using Catalog.Domain.Aggregations;
using Catalog.Domain.Files;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public partial class FileMapper
{
    [MapperIgnoreSource("Id")]
    [MapperIgnoreSource("Owner")]
    [MapProperty("Title", "CategoryTitle")]
    [MapProperty("Description", "CategoryDescription")]
    [MapProperty("Products", "Items")]
    public partial CatalogDto ToCatalogDto(CategoryWithProducts category);
}