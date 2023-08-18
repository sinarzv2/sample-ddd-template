using Common.Models;
using Common.Resources.Messages;
using Domain.Aggregates.Identity;
using Domain.SeedWork;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Commands
{
    public class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public RevokeTokenCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<FluentResult> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResult();
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
            {
                result.AddError(Errors.UserNotFound);
                return result;
            }

            user.RefreshToken.ClearToken();
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return result;
        }
    }
}
