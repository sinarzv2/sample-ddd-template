using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using System.Text.RegularExpressions;

namespace Domain.Aggregates.Identity.ValueObjects
{
    public class Password : SeedWork.ValueObject
    {
        public const int MinLength = 8;

        public const int MaxLength = 20;

        public const string RegularExpression = "^[a-zA-Z0-9_-]{8,20}$";

        public static FluentResult<Password> Create(string? value)
        {
            var result = new FluentResult<Password>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Password);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Password, MinLength);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Password, MaxLength);

                result.AddError(errorMessage);

                return result;
            }


            if (!value.Any(char.IsDigit))
            {
                var errorMessage = string.Format(Validations.ContainNumber, DataDictionary.Password);

                result.AddError(errorMessage);

                return result;
            }

            if (Regex.IsMatch(value, RegularExpression) == false)
            {
                var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.Password);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new Password(value);

            result.SetData(returnValue);

            return result;
        }

        private Password()
        {
        }

        private Password(string value) : this()
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
            return Value;
        }
    }
}
