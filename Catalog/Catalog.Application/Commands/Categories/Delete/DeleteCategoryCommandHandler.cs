using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Notifications.Enums;
using MediatR;

namespace Catalog.Application.Commands.Categories.Delete;

public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ISender mediator) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id);

        if (category is null) throw new NotFoundException("Category not found");
        
        categoryRepository.Remove(category);
        
        var snsMessageCommand = new SnsMessageCommand
        {
            OwnerId = category.Owner,
            ItemId = category.Id,
            Type = OperationType.Delete,
            ItemType = ItemType.Category
        };

        await mediator.Send(snsMessageCommand, cancellationToken)
            .ConfigureAwait(false);
    }
}