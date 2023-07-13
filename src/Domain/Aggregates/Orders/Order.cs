using Common.Models;
using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products;
using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Orders
{
    public class Order : AggregateRoot
    {
        private Order()
        {
            _items = new List<Item>();

            _payments = new List<Payment>();
        }

        public bool IsPaid { get; init; }

        private readonly List<Item> _items;

        public virtual IReadOnlyList<Item> Items => _items;

        private readonly List<Payment> _payments;

        public virtual IReadOnlyList<Payment> Payments => _payments;

        public FluentResult<Item> AddItem(Product product, Count count)
        {
            var result = new FluentResult<Item>();

            var hasAny = _items.Any(current => current.Product == product);

            if (hasAny)
            {
                result.AddError("این محصول قبلا به سبد خرید اضافه شده است!");

                return result;
            }

            var item = new Item(product, count);

            _items.Add(item);

            result.SetData(item);

            return result;
        }

        public FluentResult<Payment> BeginPayment(Price amount)
        {
            if (amount is null)
            {
                throw new ArgumentNullException(nameof(amount));
            }

            var result = new FluentResult<Payment>();

            var payment = new Payment(amount);

            _payments.Add(payment);

            result.SetData(payment);

            return result;
        }
    }
}
