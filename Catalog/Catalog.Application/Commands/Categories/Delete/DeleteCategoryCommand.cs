using MediatR;

namespace Catalog.Application.Commands.Categories.Delete;

public record DeleteCategoryCommand(string Id) : IRequest;