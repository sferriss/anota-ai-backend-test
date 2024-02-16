using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Domain.Categories.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Categories.Delete;

public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ISender mediator) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id);

        if (category is null) throw new NotFoundException("Category not found");
        
        categoryRepository.Remove(category);

        await mediator.Send(new SnsMessageCommand(category.Owner), cancellationToken)
            .ConfigureAwait(false);
    }
}