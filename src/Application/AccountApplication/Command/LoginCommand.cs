using Application.AccountApplication.ViewModels;
using Domain.SeedWork;

namespace Application.AccountApplication.Command;

public class LoginCommand : ICommand<TokenModel>
{
    public string? UserName { get; init; } 

    public string? Password { get; init; }
}