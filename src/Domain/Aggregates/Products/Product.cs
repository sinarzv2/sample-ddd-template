namespace Domain.Aggregates.Products
{
	public class Product : SeedWork.AggregateRoot
	{
		private Product() : base()
		{
		}

		public Product(ValueObjects.ProductName productName, ValueObjects.SerialNumber serialNumber) : base()
		{
			ProductName = productName;
			SerialNumber = serialNumber;
		}

		public ValueObjects.ProductName ProductName { get; private set; }

		public ValueObjects.SerialNumber SerialNumber { get; private set; }
	}
}

//https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/net-core-microservice-domain-model
