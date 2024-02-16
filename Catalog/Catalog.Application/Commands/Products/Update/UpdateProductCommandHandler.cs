using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products.Update;

public class UpdateProductCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, ISender mediator)
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var (id, title, categoryId, price, description) = request;
        
        var product = await productRepository.GetAsync(id!)
            .ConfigureAwait(false);

        if (product is null) throw new NotFoundException("Product not found");

        var category = categoryId is not null
            ? await categoryRepository.GetAsync(categoryId).ConfigureAwait(false)
            : null;
        
        if(categoryId is not null && category is null) throw new NotFoundException("Category not found");

        product.Update(title, description, price, category?.Id);
        productRepository.Update(product);
        
        var snsMessageCommand = new SnsMessageCommand
        {
            OwnerId = product.Owner,
        };
        
        await mediator.Send(snsMessageCommand, cancellationToken)
            .ConfigureAwait(false);
    }
}