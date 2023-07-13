using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Orders
{
    public class Payment : Entity
    {

        private Payment()
        {
        }

        public Payment(Price amount) : this()
        {
            Amount = amount;
        }

        public Price Amount { get; } = Price.Default;
    }
}
