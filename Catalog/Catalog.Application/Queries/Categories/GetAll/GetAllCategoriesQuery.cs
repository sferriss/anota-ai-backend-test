using MediatR;

namespace Catalog.Application.Queries.Categories.GetAll;

public record GetAllCategoriesQuery : IRequest<GetAllCategoriesQueryResult>;