using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>, IAggregateRoot
    {
        public new Guid Id { get; set; }
        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
  
}
