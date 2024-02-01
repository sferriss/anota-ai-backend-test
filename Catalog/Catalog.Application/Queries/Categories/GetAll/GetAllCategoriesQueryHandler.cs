using Catalog.Application.Mappers;
using Catalog.Domain.Categories.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Categories.GetAll;

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, CategoryMapper mapper)
    : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesQueryResult>
{
    public async Task<GetAllCategoriesQueryResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync();

        return new()
        {
            Categories = categories.Select(mapper.ToResult).ToArray()
        };
    }
}