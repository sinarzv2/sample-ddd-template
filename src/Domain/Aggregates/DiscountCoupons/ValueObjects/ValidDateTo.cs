using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SeedWork;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
	public class ValidDateTo : ValueObject
	{
        public static ValidDateTo Default = new(DateTime.MaxValue);
        public static FluentResult<ValidDateTo> Create(DateTime? value)
		{
			var result =
				new FluentResult<ValidDateTo>();

			if (value is null)
			{
				var errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

				result.AddError(errorMessage);

				return result;
			}

			if (value.Value.Date < DateTime.Now.Date)
			{
				var errorMessage = string.Format(Validations.GreaterThanOrEqualTo_FieldValue, DataDictionary.ValidDateTo, DataDictionary.CurrentDate);

				result.AddError(errorMessage);

				return result;
			}

			var returnValue = new ValidDateTo(value: value);

			result.SetData(returnValue);

			return result;
		}

        public DateTime? Value { get; }

        private ValidDateTo(DateTime? value)
        {
            Value = value?.Date;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
        public override string ToString()
        {
            if (Value is null)
            {
                return "-----";
            }

            var result = Value.Value.ToString("yyyy/MM/dd");

            return result;
        }
    }
}
