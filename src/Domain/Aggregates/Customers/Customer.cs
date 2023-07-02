using Domain.Aggregates.Companies;
using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Customers
{
    public class Customer : AggregateRoot
	{
		protected Customer()
        {
			_relations = new List<Relation>();
		}

		public Customer(FullName fullName, EmailAddress emailAddress, NationalCode nationalCode) : this()
		{
            FullName = fullName ?? throw new ArgumentNullException(paramName: nameof(fullName));
			NationalCode = nationalCode ?? throw new ArgumentNullException(paramName: nameof(nationalCode));
			EmailAddress = emailAddress ?? throw new ArgumentNullException(paramName: nameof(emailAddress));
		}

		public virtual FullName FullName { get; }  =  FullName.Default;

		public virtual EmailAddress EmailAddress { get; } = EmailAddress.Default;

		public virtual NationalCode NationalCode { get; } = NationalCode.Default;

		
		private readonly List<Relation> _relations;

		public virtual IReadOnlyList<Relation> Relations => _relations;
       

		public void AssignToCompany(Company company)
		{
			if (company is null)
			{
				throw new ArgumentNullException(paramName: nameof(company));
			}

			var hasAny = _relations.Any(current => current.Company == company);

			if (hasAny)
			{
				return;
			}

			var relation = new Relation(this, company);

			_relations.Add(relation);
		}
	}
}
