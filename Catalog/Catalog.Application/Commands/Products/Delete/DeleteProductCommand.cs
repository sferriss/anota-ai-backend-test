using MediatR;

namespace Catalog.Application.Commands.Products.Delete;

public record DeleteProductCommand(string Id) : IRequest;