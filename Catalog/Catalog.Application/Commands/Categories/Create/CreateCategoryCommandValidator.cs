using FluentValidation;

namespace Catalog.Application.Commands.Categories.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The field 'Title' can't be empty");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The field 'Description' can't be empty");
        
        RuleFor(x => x.Owner)
            .NotEmpty().WithMessage("The field 'Owner' can't be empty");
    }
}