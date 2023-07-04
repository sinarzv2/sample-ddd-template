using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.Identity
{
    public class UserClaim : IdentityUserClaim<Guid>, IAggregateRoot
    {
        public new Guid Id { get; init; }

        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
   
}
