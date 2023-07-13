using System.Text.Json.Serialization;

namespace Domain.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        protected AggregateRoot()
        {
            _domainEvents = new List<IDomainEvent>();
        }

        [JsonIgnore]
        private readonly List<IDomainEvent> _domainEvents;

        [JsonIgnore]
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        protected void RaiseDomainEvent(IDomainEvent? domainEvent)
        {
            if (domainEvent is null)
            {
                return;
            }

            _domainEvents.Add(domainEvent);
        }

        protected void RemoveDomainEvent(IDomainEvent? domainEvent)
        {
            if (domainEvent is null)
            {
                return;
            }

            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
