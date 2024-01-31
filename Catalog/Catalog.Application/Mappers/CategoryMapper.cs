using Catalog.Application.Commands.Categories.Create;
using Catalog.Domain.Categories;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public partial class CategoryMapper
{
    public partial Category CreateToDomain(CreateCategoryCommand request);
}