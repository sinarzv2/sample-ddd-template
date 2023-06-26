using Domain.Aggregates.Products.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Products
{
	public class Product : AggregateRoot
	{
		private Product()
        {
		}

		public Product(ProductName productName, SerialNumber serialNumber)
        {
			ProductName = productName;
			SerialNumber = serialNumber;
		}

		public ProductName ProductName { get; } = ProductName.Default;

		public SerialNumber SerialNumber { get; } = SerialNumber.Default;
	}
}

