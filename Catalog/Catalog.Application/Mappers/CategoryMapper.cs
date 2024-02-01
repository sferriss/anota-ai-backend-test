using Catalog.Application.Commands.Categories.Create;
using Catalog.Application.Queries.Categories.Common;
using Catalog.Domain.Categories;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public partial class CategoryMapper
{
    public partial Category CreateToDomain(CreateCategoryCommand request);
    
    public partial GetCategoryQueryResult ToResult(Category request);
}