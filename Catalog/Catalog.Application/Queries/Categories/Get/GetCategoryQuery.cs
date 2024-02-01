using Catalog.Application.Queries.Categories.Common;
using MediatR;

namespace Catalog.Application.Queries.Categories.Get;

public record GetCategoryQuery(string Id) : IRequest<GetCategoryQueryResult>;