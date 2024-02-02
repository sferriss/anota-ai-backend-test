using Catalog.Application.Mappers;
using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products.GetAll;

public class GetAllProductsQueryHandler(IProductRepository productRepository, ProductMapper mapper)
    : IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResult>
{
    public async Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync().ConfigureAwait(false);

        return new()
        {
            Products = products.Select(mapper.ToResult).ToArray()
        };
    }
}