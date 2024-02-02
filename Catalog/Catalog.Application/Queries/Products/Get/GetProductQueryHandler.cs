using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products.Common;
using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products.Get;

public class GetProductQueryHandler(IProductRepository productRepository, ProductMapper mapper)
    : IRequestHandler<GetProductQuery, GetProductQueryResult>
{
    public async Task<GetProductQueryResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(request.Id).ConfigureAwait(false);

        if (product is null) throw new NotFoundException("Product not found");

        return mapper.ToResult(product);
    }
}