using Application.UnitOfWork;
using Common.Models;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.Identity;
using Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Commands
{
    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<FluentResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResult();
            var userId = _httpContextAccessor.HttpContext?.User.Identity?.GetUserId();
            if (userId == null)
            {
                result.AddError(Errors.UserNotFound);
                return result;
            }

      
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                result.AddError(Errors.UserNotFound);
                return result;
            }
            var checkPasswoord = await _userManager.CheckPasswordAsync(user, request.CurrentPassword!);
            if (!checkPasswoord)
            {
                result.AddError(Errors.InvalidCurrentPassword);
                return result;
            }

            var passHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword!);
            var changePasswordResult = user.ChangePassword(request.NewPassword!, passHash);
            if (!changePasswordResult.IsSuccess)
            {
                result.AddErrors(changePasswordResult.Errors);
                return result;
            }
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return result;
        }
    }
}
