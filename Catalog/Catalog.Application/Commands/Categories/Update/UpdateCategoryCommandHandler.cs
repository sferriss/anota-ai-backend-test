using Catalog.Application.Exceptions;
using Catalog.Domain.Categories.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Categories.Update;

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id!);

        if (category is null) throw new NotFoundException("Category not found");

        var (title, owner, description) = request;
        
        category.UpdateCategory(title, owner, description);
        categoryRepository.Update(category);
    }
}