using FluentValidation;

namespace Catalog.Application.Commands.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The field 'Title' can't be empty");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The field 'Description' can't be empty");
        
        RuleFor(x => x.Owner)
            .NotEmpty().WithMessage("The field 'Owner' can't be empty");
        
        RuleFor(x => x.Price)
            .NotNull().WithMessage("The field 'Owner' can't be empty")
            .GreaterThan(0).WithMessage("The field 'Price' must be greater than 0");
        
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("The field 'CategoryId' can't be empty");
    }
}