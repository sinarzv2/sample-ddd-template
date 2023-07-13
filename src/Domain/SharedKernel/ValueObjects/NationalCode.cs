using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using System.Text.RegularExpressions;

namespace Domain.SharedKernel.ValueObjects
{
    public class NationalCode : SeedWork.ValueObject
    {
        public const int FixLength = 10;

        public const string RegularExpression = "^[0-9]{10}$";


        public static NationalCode Default = new(string.Empty);

        public static FluentResult<NationalCode> Create(string? value)
        {
            var result = new FluentResult<NationalCode>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.NationalCode);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.NationalCode, FixLength);

                result.AddError(errorMessage);

                return result;
            }

            if (Regex.IsMatch(value, RegularExpression) == false)
            {
                var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.NationalCode);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new NationalCode(value: value);

            result.SetData(returnValue);

            return result;
        }


        private NationalCode()
        {
        }

        protected NationalCode(string value) : this()
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