namespace Domain.SeedWork
{
    public abstract class Entity : IEntity
    {

        protected Entity()
        {
            Id = Guid.NewGuid();
        }


        public Guid Id { get; init; }


        int? _requestedHashCode;

        public bool IsTransient()
        {
            return Id == default;
        }

        public override int GetHashCode()
        {
            if (IsTransient() == false)
            {
                if (_requestedHashCode.HasValue == false)
                {
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                }

                // XOR for random distribution. See:
                // https://docs.microsoft.com/archive/blogs/ericlippert/guidelines-and-rules-for-gethashcode
                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

    }
}
