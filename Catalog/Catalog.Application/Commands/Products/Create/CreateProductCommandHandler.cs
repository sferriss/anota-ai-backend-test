using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Notifications.Enums;
using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products.Create;

public class CreateProductCommandHandler(
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    ProductMapper mapper,
    ISender mediator)
    : IRequestHandler<CreateProductCommand, CreateProductCommandResult>
{
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.CategoryId)
            .ConfigureAwait(false);

        if (category is null) throw new NotFoundException("Category not found");

        var product = mapper.ToDomain(request);
        product.SetCategory(category.Id);

        productRepository.Add(product);
        
        var snsMessageCommand = new SnsMessageCommand
        {
            OwnerId = product.Owner,
            ItemId = product.Id,
            Type = OperationType.Create,
            ItemType = ItemType.Product
        };

        await mediator.Send(snsMessageCommand, cancellationToken)
            .ConfigureAwait(false);

        return new(product.Id);
    }
}