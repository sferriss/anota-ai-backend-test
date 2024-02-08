using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products.GetAll;

public class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResult>
{
    public async Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var productsWithCategories = await productRepository.GetProductsWithCategoriesAsync(request.Owner)
            .ConfigureAwait(false);

        return new()
        {
            Products = productsWithCategories.ToArray()
        };
    }
}