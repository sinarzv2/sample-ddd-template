namespace Domain.Aggregates.Companies
{
	public class Company : SeedWork.AggregateRoot
	{
		private Company() : base()
		{
		}

		public Company(ValueObjects.CompanyName name,
			ValueObjects.NationalIdentity nationalIdentity, string address, string description) : this()
		{
			if (name is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(name));
			}

			if (name is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(nationalIdentity));
			}

			Address =
				Dtat.String.Fix(text: address);

			Description =
				Dtat.String.Fix(text: description);

			Name = name;
			NationalIdentity = nationalIdentity;
		}

		public string Address { get; set; }

		public string Description { get; private set; }

		public ValueObjects.CompanyName Name { get; private set; }

		public ValueObjects.NationalIdentity NationalIdentity { get; private set; }

		//public void Update(string name, string description)
		//{
		//}
	}
}
