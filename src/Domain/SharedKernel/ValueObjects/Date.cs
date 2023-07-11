using Domain.SeedWork;

namespace Domain.SharedKernel.ValueObjects
{
    public class Date : ValueObject
    {
        
        public static bool operator <(Date left, Date right)
        {
            return left.Value < right.Value;
        }

        public static bool operator <=(Date left, Date right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >(Date left, Date right)
        {
            return left.Value > right.Value;
        }

        public static bool operator >=(Date left, Date right)
        {
            return left.Value >= right.Value;
        }

        protected Date()
        {
        }

      
        protected Date(DateTime? value) : this()
        {
            if (value is not null)
            {
                value =
                    value.Value.Date;
            }

            Value = value;
        }

        public DateTime? Value { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            if (Value is null)
            {
                return "-----";
            }

            var result = Value.Value.ToString("yyyy/MM/dd");

            return result;
        }
    }
}
