using Application.AccountApplication.Dtos;
using Application.Common.JwtServices;
using Application.UnitOfWork;
using Common.Models;
using Common.Resources.Messages;
using Domain.Aggregates.Identity;
using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Commands;

public class RefreshTokenCommandHandler: ICommandHandler<RefreshTokenCommand,TokenDto>
{
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public RefreshTokenCommandHandler(IJwtService jwtService, UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<FluentResult<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = new FluentResult<TokenDto>();
        var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (!principal.IsSuccess)
        {
            result.AddErrors(principal.Errors);
            return result;
        }
        var user = await _userManager.FindByNameAsync(principal.Data.Identity?.Name!);
        if (user is null || user.RefreshToken.Token != request.RefreshToken ||
            user.RefreshToken.ExpiryTime <= DateTime.Now)
        {
            result.AddError(Errors.InvalidRefreshToken);
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
        await _unitOfWork.CommitChangesAsync(cancellationToken);
        result.SetData(tokenModel);
        return result;
    }
}