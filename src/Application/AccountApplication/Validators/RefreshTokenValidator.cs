using Application.AccountApplication.Commands;
using Common.Constant;
using Common.Resources;
using FluentValidation;

namespace Application.AccountApplication.Validators;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(u => u.AccessToken)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .WithName(DataDictionary.AccessToken);

        RuleFor(u => u.RefreshToken)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .WithName(DataDictionary.RefreshToken);
    }
}