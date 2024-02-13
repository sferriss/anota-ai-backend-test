using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Notifications.Enums;
using MediatR;

namespace Catalog.Application.Commands.Categories.Create;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryMapper mapper, ISender mediator)
    : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResult>
{
    public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var hasCategory = await categoryRepository.HasCategoryWithTitleAsync(request.Title)
            .ConfigureAwait(false);

        if (hasCategory) throw new BusinessValidationException("There is already a category with that name.");
        
        var newCategory = categoryRepository.Add(mapper.ToDomain(request));
        
        var snsMessageCommand = new SnsMessageCommand
        {
            OwnerId = newCategory.Owner,
            ItemId = newCategory.Id,
            Type = OperationType.Create,
            ItemType = ItemType.Category
        };

        await mediator.Send(snsMessageCommand, cancellationToken)
            .ConfigureAwait(false);

        return new CreateCategoryCommandResult (newCategory.Id);
    }
}