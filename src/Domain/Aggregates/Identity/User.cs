using Common.Models;
using Domain.Aggregates.Identity.ValueObjects;
using Domain.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Aggregates.Identity
{
    public sealed class User : IdentityUser<Guid>
    {
        public static FluentResult<User> Create(string username, string password, string emailAddress,
            string phoneNumber, int? gender, string firstName, string lastName)
        {
            var result = new FluentResult<User>();

            var usernameResult = Username.Create(username);

            result.AddErrors(usernameResult.Errors);

            var passwordResult = Password.Create(password);

            result.AddErrors(passwordResult.Errors);

            var emailAddressResult = EmailAddress.Create(emailAddress);

            result.AddErrors(emailAddressResult.Errors);

            var phoneNumberResult = SharedKernel.ValueObjects.PhoneNumber.Create(phoneNumber);

            result.AddErrors(phoneNumberResult.Errors);

            var fullNameResult = FullName.Create(gender, firstName, lastName);

            var returnValue =
                new User(usernameResult.Data, password: passwordResult.Data,
                emailAddress: emailAddressResult.Data, phoneNumberResult.Data,
                fullNameResult.Data);

            result.SetData(returnValue);

            return result;
        }
        private User()
        {
        }

        private User(Username username, Password password, EmailAddress emailAddress, PhoneNumber phoneNumber, FullName fullName) : this()
        {
            FullName = fullName;
            UserName = username.Value;
            PasswordHash = password.Value;
            Email = emailAddress.Value;
            PhoneNumber = phoneNumber.Value;
        }
        
        public FullName FullName { get; init; } = FullName.Default;
        public DateTime BirthDate { get; init; }
        public bool IsActive { get; init; }
        public string RefreshToken { get; init; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; init; }

        public FluentResult ChangePassword(string newPassword)
        {
            var result = new FluentResult();

            var newPasswordResult = Password.Create(newPassword);

            if (newPasswordResult.Errors.Any())
            {
                result.AddErrors(newPasswordResult.Errors);

                return result;
            }

            PasswordHash = newPasswordResult.Data.Value;

            //RaiseDomainEvent
            //    (new Events.UserPasswordChangedEvent
            //    (fullName: FullName, emailAddress: EmailAddress));

            return result;
        }
    }
}

