using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.SharedKernel.ValueObjects
{
    public class FirstName : ValueObject
    {

        public const int MaxLength = 50;

        public static FirstName Default = new(string.Empty);

        public static FluentResult<FirstName> Create(string? value)
        {
            var result = new FluentResult<FirstName>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format
                    (Validations.MaxLength, DataDictionary.FirstName, MaxLength);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new FirstName(value);

            result.SetData(returnValue);

            return result;
        }


        private FirstName()
        {
        }

        private FirstName(string value) : this()
        {
            Value = value;
        }

        public string Value { get; } = string.Empty;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Value) ? "-----" : Value;
        }
    }
}
