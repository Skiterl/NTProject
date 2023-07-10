using CRM.Domain.Base;
using CRM.Domain.Exceptions;
using System.Text.RegularExpressions;


namespace CRM.Domain.ValueObjects
{
    public sealed class Name : ValueObject
    {
        private static readonly Regex ValidationRegex = new Regex(
        @"^[\p{L}\p{M}\p{N}]{1,100}\z",
        RegexOptions.Singleline | RegexOptions.Compiled);

        public Name() { }
        public Name(String value)
        {
            if (!IsValid(value))
            {
                throw new DomainValidationException("Name is not valid");
            }

            Value = value;
        }

        public String Value { get; }

        public static Boolean IsValid(String value)
        {
            return !String.IsNullOrWhiteSpace(value) && ValidationRegex.IsMatch(value);
        }

        public override Boolean Equals(Object obj)
        {
            return obj is Name other &&
                   StringComparer.Ordinal.Equals(Value, other.Value);
        }

        public override Int32 GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(Value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
