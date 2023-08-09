using Application.AccountApplication.Dtos;
using Domain.SeedWork;

namespace Application.AccountApplication.Command;

public class LoginCommand : ICommand<TokenDto>
{
    public string? UserName { get; init; } 

    public string? Password { get; init; }
}