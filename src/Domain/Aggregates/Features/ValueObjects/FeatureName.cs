using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Features.ValueObjects
{
    public class FeatureName : ValueObject
    {

        public const int MaxLength = 100;


        public static FeatureName Default = new(string.Empty);

        public static FluentResult<FeatureName> Create(string? value)
        {
            var result = new FluentResult<FeatureName>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.FeatureName);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.FeatureName, MaxLength);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new FeatureName(value);

            result.SetData(returnValue);

            return result;
        }


        private FeatureName()
        {
        }

        protected FeatureName(string value) : this()
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
