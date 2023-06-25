namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
	public class DiscountPercent : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int Minimum = 0;

		public const int Maximum = 100;
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<DiscountPercent> Create(int? value)
		{
			var result =
				new FluentResults.Result<DiscountPercent>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.DiscountPercent);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if ((value < Minimum) || (value > Maximum))
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Range,
					Resources.DataDictionary.DiscountPercent, Minimum, Maximum);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new DiscountPercent(value: value);

			result.WithValue(value: returnValue);

			return result;
		}

		public static FluentResults.Result<DiscountPercent> operator +(DiscountPercent left, DiscountPercent right)
		{
			int? value =
				left.Value + right.Value;

			var result =
				Create(value: value);

			return result;
		}

		public static FluentResults.Result<DiscountPercent> operator -(DiscountPercent left, DiscountPercent right)
		{
			int? value =
				left.Value - right.Value;

			var result =
				Create(value: value);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private DiscountPercent() : base()
		{
		}

		private DiscountPercent(int? value) : this()
		{
			Value = value;
		}

		public int? Value { get; }

		protected override
			System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public override string ToString()
		{
			if(Value is null)
			{
				return "-----";
			}
			else
			{
				return Value.ToString();
			}
		}
	}
}
