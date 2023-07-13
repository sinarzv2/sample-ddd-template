using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.ValueObjects
{
    public class Count : ValueObject
    {
        public const int Minimum = 1;

        public const int Maximum = 10;

        public static Count Default = new(0);

        public static FluentResult<Count> Create(int value)
        {
            var result = new FluentResult<Count>();

            if (value < Minimum || value > Maximum)
            {
                var errorMessage = string.Format(Validations.Range, DataDictionary.Count, Minimum, Maximum);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new Count(value: value);

            result.SetData(returnValue);

            return result;
        }



        private Count()
        {
        }

        protected Count(int value) : this()
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
