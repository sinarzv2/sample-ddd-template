using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SeedWork;

namespace Domain.SharedKernel.ValueObjects
{
    public class Price : ValueObject
    {
        public const int Minimum = 0;

        public const int Maximum = 10_000_000;

        public static Price Default = new(0);

        public static FluentResult<Price> Create(int value)
        {
            var result = new FluentResult<Price>();

            if (value < Minimum || value > Maximum)
            {
                var errorMessage = string.Format(Validations.Range, DataDictionary.Price, Minimum, Maximum);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new Price(value);

            result.SetData(returnValue);

            return result;
        }

        public static FluentResult<Price> operator +(Price left, Price right)
        {
            var value = left.Value + right.Value;

            var result = Create(value);

            return result;
        }

        public static FluentResult<Price> operator -(Price left, Price right)
        {
            var value = left.Value - right.Value;

            var result = Create(value);

            return result;
        }

        private Price()
        {
        }

        protected Price(int value) : this()
        {
            Value = value;
        }

        public int Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
