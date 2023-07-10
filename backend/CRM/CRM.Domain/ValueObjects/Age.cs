using CRM.Domain.Base;
using CRM.Domain.Exceptions;

namespace CRM.Domain.ValueObjects
{
    public sealed class Age : ValueObject
    {
        public Age() { }
        public Age(int value)
        {
            if (!IsValid(value))
            {
                throw new DomainValidationException("Age is not valid");
            }

            Value = value;
        }

        public int Value { get; }

        public static Boolean IsValid(int value)
        {
            return 10 <= value && value <= 120;
        }

        public override Boolean Equals(Object obj)
        {
            return obj is Age other && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
