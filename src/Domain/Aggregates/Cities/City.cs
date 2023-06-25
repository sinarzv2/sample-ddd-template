namespace Domain.Aggregates.Cities
{
	public class City : SeedWork.AggregateRoot
	{
		#region Static Member(s)
		public static FluentResults.Result<City>
			Create(Provinces.Province province, string name)
		{
			var result =
				new FluentResults.Result<City>();

			// **************************************************
			if (province is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required,
					Resources.DataDictionary.Province);

				result.WithError(errorMessage: errorMessage);
			}
			// **************************************************

			// **************************************************
			var nameResult =
				SharedKernel.Name.Create(value: name);

			result.WithErrors(errors: nameResult.Errors);
			// **************************************************

			if (result.IsFailed)
			{
				return result;
			}

			var returnValue =
				new City(province: province, name: nameResult.Value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private City() : base()
		{
		}

		private City(Provinces.Province province, SharedKernel.Name name) : this()
		{
			Name = name;
			Province = province;
		}

		public SharedKernel.Name Name { get; private set; }

		public virtual Provinces.Province Province { get; private set; }

		public FluentResults.Result Update(string name)
		{
			var result =
				Create(province: Province, name: name);

			if (result.IsFailed)
			{
				return result.ToResult();
			}

			Name = result.Value.Name;

			return result.ToResult();
		}
	}
}
