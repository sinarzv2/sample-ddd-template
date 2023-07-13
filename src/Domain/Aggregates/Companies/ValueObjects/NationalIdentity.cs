using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Companies.ValueObjects
{
    public class NationalIdentity : ValueObject
    {
        public const int FixLength = 10;

        public const string RegularExpression = "^[0-9]{10}$";

        public static NationalIdentity Default = new(string.Empty);

        public static FluentResult<NationalIdentity> Create(string? value)
        {
            var result = new FluentResult<NationalIdentity>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format
                    (Validations.Required, DataDictionary.NationalIdentity);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.NationalIdentity, FixLength);

                result.AddError(errorMessage);

                return result;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(value, RegularExpression) == false)
            {
                var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.NationalIdentity);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new NationalIdentity(value);

            result.SetData(returnValue);

            return result;
        }


        private NationalIdentity()
        {
        }

        protected NationalIdentity(string value) : this()
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
