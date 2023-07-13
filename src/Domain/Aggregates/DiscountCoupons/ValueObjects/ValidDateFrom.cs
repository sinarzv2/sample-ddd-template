using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
    public class ValidDateFrom : Date
    {
        public static ValidDateFrom Default = new(DateTime.MinValue);
        public static FluentResult<ValidDateFrom> Create(DateTime? value)
        {
            var result = new FluentResult<ValidDateFrom>();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Value.Date < DateTime.Now.Date)
            {
                var errorMessage = string.Format(Validations.GreaterThanOrEqualTo_FieldValue, DataDictionary.ValidDateFrom, DataDictionary.CurrentDate);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new ValidDateFrom(value);

            result.SetData(returnValue);

            return result;
        }


        private ValidDateFrom(DateTime? value) : base(value)
        {
        }


    }
}
