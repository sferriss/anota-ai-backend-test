using Catalog.Application.Commands.Products.Create;
using Catalog.Domain.Aggregations;
using Catalog.Domain.Products;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public partial class ProductMapper
{
    [MapperIgnoreTarget("Category")]
    public partial Product ToDomain(CreateProductCommand request);

    [MapperIgnoreTarget("Category")]
    public partial ProductWithCategoryResult ToResult(Product request);
}