using Catalog.Domain.Aggregations;
using MediatR;

namespace Catalog.Application.Queries.Categories.Get;

public record GetCategoryQuery(string Id) : IRequest<CategoryResult>;