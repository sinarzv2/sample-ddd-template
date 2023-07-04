using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.Identity
{
    public class UserToken: IdentityUserToken<Guid>, IAggregateRoot
    {
        public Guid Id { get; init; }
        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
  
}
