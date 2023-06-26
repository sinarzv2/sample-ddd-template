using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products;

namespace Domain.Aggregates.Orders
{
    public class Item : SeedWork.Entity
	{
		
		private Item()
        {
		}

		public Item(Product product, Count count) : this()
		{
            Count = count ?? throw new ArgumentNullException(paramName: nameof(count));
			Product = product ?? throw new ArgumentNullException(paramName: nameof(product));
		}

        public Count Count { get; } = Count.Default;

		public Product Product { get; }
	}
}
