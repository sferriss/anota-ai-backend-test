﻿using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
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
        var category = await categoryRepository.GetAsync(request.CategoryId)
            .ConfigureAwait(false);

        if (category is null) throw new NotFoundException("Category not found");

        var product = mapper.ToDomain(request);
        product.SetCategory(category);

        productRepository.Add(product);

        await mediator.Send(new SnsMessageCommand { OwnerId = product.Owner }, cancellationToken)
            .ConfigureAwait(false);

        return new(product.Id);
    }
}