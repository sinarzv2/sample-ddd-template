using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Aggregates.Identity
{
    public class UserToken: IdentityUserToken<Guid>, IAggregateRoot
    {
        [NotMapped]
        [JsonIgnore]
        public Guid Id { get; init; }
        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
  
}
