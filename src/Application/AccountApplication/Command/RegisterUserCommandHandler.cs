using Common.Constant;
using Common.Models;
using Domain.Aggregates.Identity;
using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.AccountApplication.Command
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public RegisterUserCommandHandler(UserManager<User> userManager, IOptionsSnapshot<SiteSettings> siteSetting)
        {
            _userManager = userManager;
            _jwtSettings = siteSetting.Value.JwtSettings;
        }

        public async Task<FluentResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResult();

            var userResult = User.Create(request.UserName, request.Password, request.Email, request.PhoneNumber,
                request.Gender, request.FirstName, request.LastName, request.BirthDate, _jwtSettings.ExpirationRefreshTimeDays);
            if (!userResult.IsSuccess)
            {
                result.AddErrors(userResult.Errors);
                return result;
            }

            var user = userResult.Data;
            var identityResult = await _userManager.CreateAsync(user, request.Password!);
            if (!identityResult.Succeeded)
            {
                var allError = identityResult.Errors.Select(d => d.Description);
                result.AddErrors(allError);
                return result;
            }
            var resultUserRole = await _userManager.AddToRoleAsync(user, ConstantRoles.User);
            if (!resultUserRole.Succeeded)
            {
                var allError = resultUserRole.Errors.Select(d => d.Description);
                result.AddErrors(allError);
                return result;
            }
            return result;
        }
    }
}
