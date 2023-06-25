namespace Domain.Aggregates.Companies.ValueObjects
{
	public class NationalIdentity : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int FixLength = 10;

		public const string RegularExpression = "^[0-9]{10}$";
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<NationalIdentity> Create(string value)
		{
			var result =
				new FluentResults.Result<NationalIdentity>();

			value =
				Dtat.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.NationalIdentity);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length != FixLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.FixLengthNumeric,
					Resources.DataDictionary.NationalIdentity, FixLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (System.Text.RegularExpressions.Regex.IsMatch
				(input: value, pattern: RegularExpression) == false)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.RegularExpression, Resources.DataDictionary.NationalIdentity);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new NationalIdentity(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private NationalIdentity() : base()
		{
		}

		protected NationalIdentity(string value) : this()
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
