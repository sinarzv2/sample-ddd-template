namespace Domain.Aggregates.Products.ValueObjects
{
	public class SerialNumber : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int FixLength = 20;

		public const string RegularExpression = "^[0-9]{20}$";
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<SerialNumber> Create(string value)
		{
			var result =
				new FluentResults.Result<SerialNumber>();

			value =
				Dtat.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.SerialNumber);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length != FixLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.FixLengthNumeric,
					Resources.DataDictionary.SerialNumber, FixLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (System.Text.RegularExpressions.Regex.IsMatch
				(input: value, pattern: RegularExpression) == false)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.RegularExpression, Resources.DataDictionary.SerialNumber);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new SerialNumber(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private SerialNumber() : base()
		{
		}

		private SerialNumber(string value) : this()
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
