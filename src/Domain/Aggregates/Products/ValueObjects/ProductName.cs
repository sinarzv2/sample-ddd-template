using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Products.ValueObjects
{
    public class ProductName : ValueObject
    {
        public const int MaxLength = 100;

        public static ProductName Default = new(string.Empty);

        public static FluentResult<ProductName> Create(string? value)
        {
            var result = new FluentResult<ProductName>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.ProductName);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.ProductName, MaxLength);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new ProductName(value);

            result.SetData(returnValue);

            return result;
        }

        private ProductName()
        {
        }

        private ProductName(string value) : this()
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
