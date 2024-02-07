using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Aggregations;
using Catalog.Domain.Categories.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Categories.Get;

public class GetCategoryQueryHandler(ICategoryRepository categoryRepository, CategoryMapper mapper)
    : IRequestHandler<GetCategoryQuery, CategoryResult>
{
    public async Task<CategoryResult> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id).ConfigureAwait(false);

        if (category is null) throw new NotFoundException("Category not found");

        return mapper.ToResult(category);
    }
}