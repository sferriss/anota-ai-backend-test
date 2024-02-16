using Catalog.Application.Commands.Aws.Sns;
using Catalog.Application.Exceptions;
using Catalog.Domain.Categories.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Categories.Update;

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ISender mediator) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id!);

        if (category is null) throw new NotFoundException("Category not found");

        var (title, description) = request;
        
        category.Update(title, description);
        
        categoryRepository.Update(category);

        await mediator.Send(new SnsMessageCommand(category.Owner), cancellationToken)
            .ConfigureAwait(false);
    }
}