using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.SharedKernel.ValueObjects
{
    public class LastName : ValueObject
    {
       
        public const int MaxLength = 50;
        

        
        public static FluentResult<LastName> Create(string? value)
        {
            var result = new FluentResult<LastName>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.LastName);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.LastName, MaxLength);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new LastName(value: value);

            result.SetData(returnValue);

            return result;
        }

        private LastName()
        {
        }

        private LastName(string value) : this()
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
