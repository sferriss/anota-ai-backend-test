using Catalog.Domain.Aggregations.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products.GetAll;

public class GetAllProductsQueryHandler(IAggregationsRepository aggregationsRepository) : IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResult>
{
    public async Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var productsWithCategories = await aggregationsRepository.GetProductsWithCategoriesAsync()
            .ConfigureAwait(false);

        return new()
        {
            Products = productsWithCategories.ToArray()
        };
    }
}