using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Products.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products.Update;

public class UpdateProductCommandHandler(
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    ISender mediator)
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

        if (categoryId is not null && category is null) throw new NotFoundException("Category not found");

        if (!category!.Owner.Equals(product.Owner))
            throw new BusinessValidationException("Category owner and product owner must be the same");

        product.Update(title, description, price, category.Id);
        productRepository.Update(product);

        await mediator.Send(new SnsMessageCommand(product.Owner), cancellationToken)
            .ConfigureAwait(false);
    }
}