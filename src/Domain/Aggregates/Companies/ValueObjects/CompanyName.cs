namespace Domain.Aggregates.Companies.ValueObjects
{
	public class CompanyName : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int MaxLength = 10;
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<CompanyName> Create(string value)
		{
			var result =
				new FluentResults.Result<CompanyName>();

			value =
				Dtat.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.CompanyName);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MaxLength, Resources.DataDictionary.CompanyName, MaxLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new CompanyName(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private CompanyName() : base()
		{
		}

		protected CompanyName(string value) : this()
		{
			Value = value;
		}

		public string Value { get; private set; }

		protected override
			System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
