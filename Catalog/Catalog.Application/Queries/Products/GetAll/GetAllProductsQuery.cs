using MediatR;

namespace Catalog.Application.Queries.Products.GetAll;

public record GetAllProductsQuery(string Owner) : IRequest<GetAllProductsQueryResult>;