using Application.AccountApplication.Dtos;
using Application.Common.JwtServices;
using Common.Models;
using Common.Resources.Messages;
using Domain.Aggregates.Identity;
using Domain.SeedWork;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Command;

public class LoginCommandHandler : ICommandHandler<LoginCommand,TokenDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(UserManager<User> userManager, IJwtService jwtService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }

    public async Task<FluentResult<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = new FluentResult<TokenDto>();
        var user = await _userManager.FindByNameAsync(request.UserName!);
        if (user == null)
        {
            result.AddError(Errors.InvalidUsernameOrPassword);
            return result;
        }

        var isPassValid = await _userManager.CheckPasswordAsync(user, request.Password!);
        if (!isPassValid)
        {
            result.AddError(Errors.InvalidUsernameOrPassword);
            return result;
        }

        var tokenModel = await _jwtService.GenerateAsync(user);
        var refreshTokenResult = user.SetNewRefreshToken();
        if (!refreshTokenResult.IsSuccess)
        {
            result.AddErrors(refreshTokenResult.Errors);
            return result;
        }
        tokenModel.RefreshToken = user.RefreshToken.Token;
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitChangesAsync(cancellationToken);
        result.SetData(tokenModel);
        return result;
    }
}