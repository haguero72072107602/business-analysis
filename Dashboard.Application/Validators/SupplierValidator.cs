using Dashboard.Domain.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators;

public class SupplierValidator : AbstractValidator<Supplier>
{
    public SupplierValidator()
    {
        RuleFor(p => p.Code).NotEmpty().WithMessage("Please provide a code");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Please provide a descripcion");
        RuleFor(p => p.Email).EmailAddress().WithMessage("incorrect email account");
    }
    
}