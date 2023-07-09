using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.Identity
{
    public class UserRole:IdentityUserRole<Guid>, IEntity
    {
      
        [NotMapped]
        [JsonIgnore]
        public Guid Id { get; init; }
    
    }
 
}
