using Domain.SeedWork;

namespace Application.AccountApplication.Commands
{
    public class RevokeTokenCommand : ICommand
    {
        public RevokeTokenCommand(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; init; }
    }
}
