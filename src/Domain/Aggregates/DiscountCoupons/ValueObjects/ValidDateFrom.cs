using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SeedWork;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
	public class ValidDateFrom : ValueObject
	{
        public static ValidDateFrom Default = new(DateTime.MinValue);
        public static FluentResult<ValidDateFrom> Create(DateTime? value)
		{
			var result =
				new FluentResult<ValidDateFrom>();

			if (value is null)
			{
				var errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

				result.AddError(errorMessage);

				return result;
			}

			if (value.Value.Date < DateTime.Now.Date)
			{
				var errorMessage = string.Format
					(Validations.GreaterThanOrEqualTo_FieldValue, DataDictionary.ValidDateFrom, DataDictionary.CurrentDate);

				result.AddError(errorMessage);

				return result;
			}

			var returnValue = new ValidDateFrom(value);

			result.SetData(returnValue);

			return result;
		}

        public DateTime? Value { get; }

        private ValidDateFrom(DateTime? value)
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
