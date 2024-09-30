using Dashboard.Domain.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators;

public class UserValidator : AbstractValidator<Users>
{
    public UserValidator()
    {
        RuleFor(p => p.UserName).NotEmpty().WithMessage("Please provide a user name");
        RuleFor(p => p.Password).NotEmpty().WithMessage("Please provide a password");
        RuleFor(p => p.Role).NotEmpty().WithMessage("Please provide a role");
        
        RuleFor(p => p.ConfirmedPswd)
            .Must(p => !string.IsNullOrEmpty(p)).WithMessage("Please confirmed password")
            .Equal(p => p.Password).WithMessage("Password not valid");
    }
}