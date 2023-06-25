namespace Domain.Aggregates.Products.ValueObjects
{
	public class ProductName : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int MaxLength = 100;
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<ProductName> Create(string value)
		{
			var result =
				new FluentResults.Result<ProductName>();

			value =
				Dtat.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.ProductName);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MaxLength, Resources.DataDictionary.ProductName, MaxLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new ProductName(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private ProductName() : base()
		{
		}

		private ProductName(string value) : this()
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
