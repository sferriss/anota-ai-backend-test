using MediatR;

namespace Catalog.Application.Queries.Catalog.Get;

public record GetCatalogQuery(string OwnerId) : IRequest<GetCatalogQueryResult>;