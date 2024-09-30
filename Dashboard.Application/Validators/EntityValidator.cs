using Dashboard.Domain.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators;

public class EntityValidator : AbstractValidator<Entities>
{
    public EntityValidator()
    {
        RuleFor(p => p.Code).NotEmpty().WithMessage("Please provide a code");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Please provide a descripcion");
    }
}