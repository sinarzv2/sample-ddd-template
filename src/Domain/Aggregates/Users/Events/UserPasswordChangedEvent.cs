using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Users.Events
{
    public sealed class UserPasswordChangedEvent : object, Dtat.Ddd.IDomainEvent
	{
		public UserPasswordChangedEvent
			(SharedKernel.FullName fullName, EmailAddress emailAddress) : base()
		{
			FullName = fullName;
			EmailAddress = emailAddress;
		}

		public SharedKernel.FullName FullName { get; }

		public EmailAddress EmailAddress { get; }
	}
}
