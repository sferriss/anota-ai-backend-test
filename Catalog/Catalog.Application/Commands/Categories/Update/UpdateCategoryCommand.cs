using MediatR;

namespace Catalog.Application.Commands.Categories.Update;

public class UpdateCategoryCommand : IRequest
{
    public string? Id { get; private set; }
    
    public string? Title { get; init; }
    
    public string?  Owner { get; init; }

    public string?  Description { get; init; }

    public UpdateCategoryCommand WithId(string id)
    {
        Id = id;
        return this;
    }
    
    public void Deconstruct(out string? title, out string? owner, out string? description)
    {
        title = Title;
        owner = Owner;
        description = Description;
    }
}