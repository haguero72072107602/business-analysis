using Dashboard.Domain.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators;

public class ProdOfferingValidator : AbstractValidator<ProductOffering>
{
    public ProdOfferingValidator()
    {
        RuleFor(p => p.Supc).NotEmpty().WithMessage("Please provide a supc");
        RuleFor(p => p.Desc).NotEmpty().WithMessage("Please provide a desc");
        /*RuleFor(p => p.Price).LessThan(0).WithMessage("Please provide a price");;*/
    }    
}