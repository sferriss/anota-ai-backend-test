using MediatR;

namespace Catalog.Application.Commands.Categories.Create;

public class CreateCategoryCommand : IRequest<CreateCategoryCommandResult>
{
    public required string Title { get; init; }
    
    public required string Owner { get; init; }
    
    public required string Description { get; init; }
}