namespace Domain.Aggregates.Users.ValueObjects
{
	public class Username : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int MinLength = 8;

		public const int MaxLength = 20;

		public const string RegularExpression = "^[a-zA-Z0-9_-]{8,20}$";
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<Username> Create(string value)
		{
			var result =
				new FluentResults.Result<Username>();

			value =
				Dtat.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.Username);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length < MinLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MinLength,
					Resources.DataDictionary.Username, MinLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MaxLength,
					Resources.DataDictionary.Username, MaxLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (System.Text.RegularExpressions.Regex.IsMatch
				(input: value, pattern: RegularExpression) == false)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.RegularExpression,
					Resources.DataDictionary.Username);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new Username(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private Username() : base()
		{
		}

		private Username(string value) : this()
		{
			Value = value;
		}

		public string Value { get; }

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
