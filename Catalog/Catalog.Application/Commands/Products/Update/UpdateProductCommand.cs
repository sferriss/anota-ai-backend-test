using MediatR;

namespace Catalog.Application.Commands.Products.Update;

public class UpdateProductCommand : IRequest
{
    public string? Id { get; private set; }
    
    public string? Title { get; init; }
    
    public string? CategoryId { get; init; }
    
    public decimal? Price { get; init; }

    public string? Description { get; init; }
    
    public void Deconstruct(out string? id, out string? title, out string? categoryId, out decimal? price, out string? description)
    {
        id = Id;
        title = Title;
        categoryId = CategoryId;
        price = Price;
        description = Description;
    }
    
    public UpdateProductCommand WithId(string id)
    {
        Id = id;
        return this;
    }
}