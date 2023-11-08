using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.Identity;

public class UserClaim : IdentityUserClaim<Guid>, IEntity
{
    public new Guid Id { get; init; }


}