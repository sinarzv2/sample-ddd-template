using Application.AccountApplication.Commands;
using Common.Constant;
using Common.Resources;
using FluentValidation;

namespace Application.AccountApplication.Validators;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(u => u.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .WithName(DataDictionary.Username);

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .WithName(DataDictionary.Password);
    }
}