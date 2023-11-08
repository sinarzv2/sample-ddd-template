using Application.AccountApplication.Dtos;
using Domain.SeedWork;

namespace Application.AccountApplication.Commands;

public class RefreshTokenCommand : ICommand<TokenDto>
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
}