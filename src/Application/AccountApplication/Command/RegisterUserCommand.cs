using Domain.SeedWork;

namespace Application.AccountApplication.Command
{
    public class RegisterUserCommand : ICommand
    {
        public string? UserName { get; init; }

        public string? Password { get; init; }

        public string? ConfirmPassword { get; init; }

        public string? PhoneNumber { get; init; }

        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public int? Gender { get; init; }

        public string? Email { get; init; }

        public DateTime? BirthDate { get; init; }
    }
}
