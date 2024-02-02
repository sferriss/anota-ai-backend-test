using Catalog.Application.Queries.Products.Common;
using MediatR;

namespace Catalog.Application.Queries.Products.Get;

public record GetProductQuery(string Id) : IRequest<GetProductQueryResult>;