using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>, IEntity
    {
        public new Guid Id { get; set; }

    }
  
}
