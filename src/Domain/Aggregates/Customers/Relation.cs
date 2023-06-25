namespace Domain.Aggregates.Customers
{
	public class Relation : SeedWork.Entity
	{
		/// <summary>
		/// For EF Core!
		/// </summary>
		protected Relation() : base()
		{
		}

		public Relation(Customer customer, Companies.Company company) : this()
		{
			if (company is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(company));
			}

			if (customer is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(customer));
			}

			Company = company;
			Customer = customer;
		}

		public Customer Customer { get; private set; }

		public Companies.Company Company { get; private set; }
	}
}
