using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Identity.Events
{
    public sealed class UserPasswordChangedEvent : IDomainEvent
    {
        public UserPasswordChangedEvent(FullName fullName, string? emailAddress)
        {
            FullName = fullName;
            EmailAddress = emailAddress;
        }

        public FullName FullName { get; }

        public string? EmailAddress { get; }
    }
}
