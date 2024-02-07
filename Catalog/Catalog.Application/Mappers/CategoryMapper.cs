using Catalog.Application.Commands.Categories.Create;
using Catalog.Domain.Aggregations;
using Catalog.Domain.Categories;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public partial class CategoryMapper
{
    public partial Category ToDomain(CreateCategoryCommand request);
    
    public partial CategoryResult ToResult(Category request);
}