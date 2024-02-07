using Catalog.Domain.Aggregations;
using MediatR;

namespace Catalog.Application.Queries.Products.Get;

public record GetProductQuery(string Id) : IRequest<ProductWithCategoryResult>;