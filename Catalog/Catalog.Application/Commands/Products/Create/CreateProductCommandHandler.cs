using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Categories;
using Catalog.Domain.Categories.Repositories;
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
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var category = await ValidateAsync(request).ConfigureAwait(false);

        var product = mapper.ToDomain(request);
        product.SetCategory(category.Id);

        productRepository.Add(product);

        await mediator.Send(new SnsMessageCommand(product.Owner), cancellationToken)
            .ConfigureAwait(false);

        return new(product.Id);
    }

    private async Task<Category> ValidateAsync(CreateProductCommand request)
    {
        var category = await categoryRepository.GetAsync(request.CategoryId)
            .ConfigureAwait(false);

        if (category is null) throw new NotFoundException("Category not found");
        
        if (!category.Owner.Equals(request.Owner))
            throw new BusinessValidationException("Category owner and product owner must be the same");

        return category;
    }
}