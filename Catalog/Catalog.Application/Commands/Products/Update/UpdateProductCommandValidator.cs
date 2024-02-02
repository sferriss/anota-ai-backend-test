using FluentValidation;

namespace Catalog.Application.Commands.Products.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        When(x => x.Price is not null, () =>
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("The field 'Price' must be greater than 0");
        });
    }
}