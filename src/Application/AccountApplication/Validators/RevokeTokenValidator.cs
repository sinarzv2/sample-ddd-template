using Application.AccountApplication.Commands;
using Common.Constant;
using Common.Resources;
using FluentValidation;

namespace Application.AccountApplication.Validators
{
    public class RevokeTokenValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty()
                    .WithMessage(ConstantValidation.Required)
                .WithName(DataDictionary.UserId);
        }
    }
}
