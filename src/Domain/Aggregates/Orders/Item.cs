namespace Domain.Aggregates.Orders
{
	public class Item : SeedWork.Entity
	{
		/// <summary>
		/// For EF Core!
		/// </summary>
		private Item() : base()
		{
		}

		public Item(Products.Product product, Count count) : this()
		{
			if (count is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(count));
			}

			if (product is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(product));
			}

			Count = count;
			Product = product;
		}

		public Count Count { get; private set; }

		public Products.Product Product { get; private set; }
	}
}
