﻿using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Application.Queries.Categories.Common;
using Catalog.Domain.Categories.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Categories.Get;

public class GetCategoryQueryHandler(ICategoryRepository categoryRepository, CategoryMapper mapper)
    : IRequestHandler<GetCategoryQuery, GetCategoryQueryResult>
{
    public async Task<GetCategoryQueryResult> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id);

        if (category is null) throw new NotFoundException("Category not found");

        return mapper.ToResult(category);
    }
}