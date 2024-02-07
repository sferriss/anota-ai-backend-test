using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Aggregations;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products.Get;

public class GetProductQueryHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    ProductMapper mapper,
    CategoryMapper categoryMapper)
    : IRequestHandler<GetProductQuery, ProductWithCategoryResult>
{
    public async Task<ProductWithCategoryResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(request.Id).ConfigureAwait(false);

        if (product is null) throw new NotFoundException("Product not found");

        var category = await categoryRepository.GetAsync(product.CategoryId).ConfigureAwait(false);

        var categoryResult = categoryMapper.ToResult(category);
        var productResult = mapper.ToResult(product);

        productResult.Category = categoryResult;

        return productResult;
    }
}