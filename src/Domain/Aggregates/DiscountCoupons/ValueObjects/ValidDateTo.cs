using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
    public class ValidDateTo : Date
    {
        public static ValidDateTo Default = new(DateTime.MaxValue);
        public static FluentResult<ValidDateTo> Create(DateTime? value)
        {
            var result = new FluentResult<ValidDateTo>();

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



        private ValidDateTo(DateTime? value) : base(value)
        {
        }

    }
}
