namespace Domain.Aggregates.Packages.ValueObjects
{
	public class PackageName : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int MaxLength = 100;
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<PackageName> Create(string value)
		{
			var result =
				new FluentResults.Result<PackageName>();

			value =
				Dtat.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.PackageName);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MaxLength,
					Resources.DataDictionary.PackageName, MaxLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new PackageName(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private PackageName() : base()
		{
		}

		protected PackageName(string value) : this()
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
