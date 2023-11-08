using Application.Common;
using Common.Constant;
using Common.Resources;
using Domain.Aggregates.Identity.ValueObjects;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;
using Application.AccountApplication.Commands;
using Application.AccountApplication.Services;
using Common.Resources.Messages;


namespace Application.AccountApplication.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator(IUserService userService)
    {

        RuleFor(u => u.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .MinimumLength(Username.MinLength)
            .WithMessage(ConstantValidation.MinLength)
            .MaximumLength(Username.MaxLength)
            .WithMessage(ConstantValidation.MaxLength)
            .Matches(new Regex(Username.RegularExpression))
            .WithMessage(ConstantValidation.RegularExpression)
            .MustAsync(async (username, _) => !(await userService.ExistUserByUsername(username)))
            .WithMessage(Errors.DublicateUsername)
            .WithName(DataDictionary.Username);

        RuleFor(u => u.Password)
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
            .WithName(DataDictionary.Password);

        RuleFor(u => u.ConfirmPassword)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .Must((model,confirmPassword)=> model.Password == confirmPassword)
            .WithMessage(Validations.ConfirmPassword)
            .WithName(DataDictionary.ConfirmPassword);

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .MaximumLength(EmailAddress.MaxLength)
            .WithMessage(ConstantValidation.MaxLength)
            .EmailAddress()
            .WithMessage(ConstantValidation.RegularExpression)
            .WithName(DataDictionary.EmailAddress);


        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .Length(PhoneNumber.FixLength)
            .WithMessage(ConstantValidation.FixLengthNumeric(PhoneNumber.FixLength))
            .Matches(new Regex(PhoneNumber.RegularExpression))
            .WithMessage(ConstantValidation.RegularExpression)
            .WithName(DataDictionary.PhoneNumber);

        RuleFor(u => u.BirthDate)
            .NotNull()
            .WithMessage(ConstantValidation.Required)
            .InclusiveBetween(BirthDate.MinValue, BirthDate.MaxValue)
            .WithMessage(ConstantValidation.Range)
            .WithName(DataDictionary.Birthdate);

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .MaximumLength(FirstName.MaxLength)
            .WithMessage(ConstantValidation.MaxLength)
            .WithName(DataDictionary.FirstName);

        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage(ConstantValidation.Required)
            .MaximumLength(LastName.MaxLength)
            .WithMessage(ConstantValidation.MaxLength)
            .WithName(DataDictionary.LastName);

        RuleFor(u => u.Gender)
            .NotNull()
            .WithMessage(ConstantValidation.Required)
            .Must(GeneralValidators.ValidEnum<Gender>())
            .WithMessage(ConstantValidation.InvalidCode)
            .WithName(DataDictionary.Gender);
    }


}