using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Aggregates.Identity;

public class UserToken : IdentityUserToken<Guid>, IEntity
{
    [NotMapped]
    [JsonIgnore]
    public Guid Id { get; init; }

}