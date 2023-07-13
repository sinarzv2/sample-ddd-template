using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Identity.ValueObjects
{
    public class BirthDate : Date
    {
        public static DateTime MinValue = new(1920, 1, 1);
        public static DateTime MaxValue = new(2020, 1, 1);

        public static BirthDate Default = new(MinValue);
        public static FluentResult<BirthDate> Create(DateTime? value)
        {
            var result = new FluentResult<BirthDate>();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Birthdate);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Value.Date < MinValue.Date || value.Value.Date > MaxValue.Date)
            {
                var errorMessage = string.Format(Validations.Range, DataDictionary.Birthdate, MinValue, MaxValue);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new BirthDate(value);

            result.SetData(returnValue);

            return result;
        }

        private BirthDate()
        {
        }

        private BirthDate(DateTime? value) : base(value)
        {
        }
    }
}
