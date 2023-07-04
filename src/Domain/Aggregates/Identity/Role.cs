using Common.Models;
using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Domain.Aggregates.Identity
{
    public sealed class Role : IdentityRole<Guid>, IAggregateRoot
    {
        [JsonIgnore]
        private readonly List<IDomainEvent> _domainEvents;

        [JsonIgnore]
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public static FluentResult<Role> Create(string? name)
        {
            var result = new FluentResult<Role>();

            var nameResult = SharedKernel.ValueObjects.Name.Create(name);

            result.AddErrors(nameResult.Errors);

            var returnValue = new Role(nameResult.Data);

            result.SetData(returnValue);

            return result;
           
        }

        private Role()
        {
            _domainEvents = new List<IDomainEvent>();
        }
        private Role(Name name) : this()
        {
            Name = name.Value;
            NormalizedName = name.Value.ToUpper();
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

    }
}
