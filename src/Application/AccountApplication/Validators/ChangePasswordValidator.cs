using Application.AccountApplication.Commands;
using Common.Constant;
using Common.Resources;
using Common.Resources.Messages;
using Domain.Aggregates.Identity.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.AccountApplication.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(u => u.CurrentPassword)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .WithName(DataDictionary.CurrentPassword);

        RuleFor(u => u.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .MinimumLength(Password.MinLength)
            .WithMessage(ConstantValidation.MinLength)
            .MaximumLength(Password.MaxLength)
            .WithMessage(ConstantValidation.MaxLength)
            .Matches(new Regex(Password.RegularExpression))
            .WithMessage(ConstantValidation.RegularExpression)
            .Must(d => d.Any(char.IsDigit))
            .WithMessage(ConstantValidation.ContainNumber)
            .WithName(DataDictionary.NewPassword);

        RuleFor(u => u.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .Must((model, confirmPassword) => model.NewPassword == confirmPassword)
            .WithMessage(Validations.ConfirmPassword)
            .WithName(DataDictionary.ConfirmPassword);
    }
}