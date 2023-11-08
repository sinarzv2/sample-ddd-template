using Domain.SeedWork;

namespace Application.AccountApplication.Commands;

public class ChangePasswordCommand : ICommand
{
    public string? CurrentPassword { get; init; }
    public string? NewPassword { get; init; }
    public string? ConfirmNewPassword { get; init; }
}