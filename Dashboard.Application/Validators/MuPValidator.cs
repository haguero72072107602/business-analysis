using Dashboard.Domain.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators;

public class MuPValidator : AbstractValidator<MUP>
{
    public MuPValidator()
    {
        RuleFor(p => p.Code).NotEmpty().WithMessage("Please provide a code");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Please provide a description");
        //RuleFor(p => p.Price).LessThan(0).WithMessage("Please provide a price");
    }
}