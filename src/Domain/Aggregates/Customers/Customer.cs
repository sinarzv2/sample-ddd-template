using System.Linq;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Customers
{
    public class Customer : SeedWork.AggregateRoot
	{
		protected Customer() : base()
		{
			_relations =
				new System.Collections.Generic.List<Relation>();
		}

		public Customer
			(FullName fullName,
			EmailAddress emailAddress,
			SharedKernel.NationalCode nationalCode) : this()
		{
			if (fullName is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(fullName));
			}

			if (emailAddress is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(emailAddress));
			}

			if (nationalCode is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(nationalCode));
			}

			FullName = fullName;
			NationalCode = nationalCode;
			EmailAddress = emailAddress;
		}

		public virtual FullName FullName { get; private set; }

		public virtual EmailAddress EmailAddress { get; private set; }

		public virtual SharedKernel.NationalCode NationalCode { get; private set; }

		// **********
		private readonly System.Collections.Generic.List<Relation> _relations;

		public virtual System.Collections.Generic.IReadOnlyList<Relation> Relations
		{
			get
			{
				return _relations;
			}
		}
		// **********

		public void AssignToCompany(Companies.Company company)
		{
			if (company is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(company));
			}

			var hasAny =
				_relations
				.Where(current => current.Company == company)
				.Any();

			if (hasAny)
			{
				return;
			}

			var relation =
				new Relation(customer: this, company: company);

			_relations.Add(relation);
		}
	}
}
