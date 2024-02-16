using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products.Delete;

public class DeleteProductCommandHandler(IProductRepository productRepository, ISender mediator) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(request.Id)
            .ConfigureAwait(false);
        
        if (product is null) throw new NotFoundException("Product not found");
        
        productRepository.Remove(product);

        var snsMessageCommand = new SnsMessageCommand
        {
            OwnerId = product.Owner,
        };
        
        await mediator.Send(snsMessageCommand, cancellationToken)
            .ConfigureAwait(false);
    }
}