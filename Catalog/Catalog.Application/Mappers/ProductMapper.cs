using Catalog.Application.Commands.Products.Create;
using Catalog.Application.Queries.Products.Common;
using Catalog.Domain.Products;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public partial class ProductMapper
{
    [MapperIgnoreTarget("Category")]
    public partial Product ToDomain(CreateProductCommand request);

    public partial GetProductQueryResult ToResult(Product request);
}