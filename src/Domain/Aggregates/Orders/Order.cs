using System.Linq;

namespace Domain.Aggregates.Orders
{
	public class Order : SeedWork.AggregateRoot
	{
		private Order()
		{
			_items =
				new System.Collections.Generic.List<Item>();

			_payments =
				new System.Collections.Generic.List<Payment>();
		}

		public bool IsPaid { get; private set; }

		// **********
		private readonly System.Collections.Generic.List<Item> _items;

		public virtual System.Collections.Generic.IReadOnlyList<Item> Items
		{
			get
			{
				return _items;
			}
		}
		// **********

		// **********
		private readonly System.Collections.Generic.List<Payment> _payments;

		public virtual System.Collections.Generic.IReadOnlyList<Payment> Payments
		{
			get
			{
				return _payments;
			}
		}
		// **********

		public FluentResults.Result<Item> AddItem(Products.Product product, Count count)
		{
			var result =
				new FluentResults.Result<Item>();

			var hasAny =
				_items
				.Where(current => current.Product == product)
				.Any();

			if (hasAny)
			{
				result.WithError
					(errorMessage: "این محصول قبلا به سبد خرید اضافه شده است!");

				return result;
			}

			var item =
				new Item(product, count);

			_items.Add(item);

			result.WithValue(value: item);

			return result;
		}

		public FluentResults.Result<Payment> BeginPayment(SharedKernel.Money amount)
		{
			if (amount is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(amount));
			}

			var result =
				new FluentResults.Result<Payment>();

			var payment =
				new Payment(amount);

			result.WithValue(value: payment);

			return result;
		}
	}
}
