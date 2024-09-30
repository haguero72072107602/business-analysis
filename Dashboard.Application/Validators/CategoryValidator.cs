using Dashboard.Domain.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators;

public class CategoryValidator : AbstractValidator<Categories>
{
    public CategoryValidator()
    {
        //RuleFor(p => p.Code).NotEmpty().WithMessage("Please provide a code");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Please provide a descripcion");
    }
}