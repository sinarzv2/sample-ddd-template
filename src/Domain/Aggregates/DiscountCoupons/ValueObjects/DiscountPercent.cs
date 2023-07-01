using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SeedWork;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
	public class DiscountPercent : ValueObject
	{
		public const int Minimum = 0;

		public const int Maximum = 100;

        public static DiscountPercent Default = new(0);

        public static FluentResult<DiscountPercent> Create(int? value)
		{
			var result =
				new FluentResult<DiscountPercent>();

			if (value is null)
			{
				var errorMessage = string.Format(Validations.Required, DataDictionary.DiscountPercent);

				result.AddError(errorMessage);

				return result;
			}

			if ((value < Minimum) || (value > Maximum))
			{
				var errorMessage = string.Format(Validations.Range, DataDictionary.DiscountPercent, Minimum, Maximum);

				result.AddError(errorMessage);

				return result;
			}

			var returnValue = new DiscountPercent(value);

			result.SetData(returnValue);

			return result;
		}

		public static FluentResult<DiscountPercent> operator +(DiscountPercent left, DiscountPercent right)
		{
			var value = left.Value + right.Value;

			var result = Create(value);

			return result;
		}

		public static FluentResult<DiscountPercent> operator -(DiscountPercent left, DiscountPercent right)
		{
			var value = left.Value - right.Value;

			var result = Create(value: value);

			return result;
		}

	
		private DiscountPercent()
        {
		}

		private DiscountPercent(int? value) : this()
		{
			Value = value;
		}

		public int? Value { get; }

		protected override IEnumerable<object?> GetEqualityComponents()
		{
			yield return Value;
		}

		public override string? ToString()
        {
            return Value is null ? "-----" : Value.ToString();
        }
	}
}
