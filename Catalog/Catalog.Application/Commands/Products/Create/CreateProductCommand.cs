using MediatR;

namespace Catalog.Application.Commands.Products.Create;

public class CreateProductCommand : IRequest<CreateProductCommandResult>
{
    public required string Title { get; init; }
    
    public required string Owner { get; init; }
    
    public required string CategoryId { get; init; }
    
    public required decimal Price { get; init; }
    
    public required string Description { get; init; }
}