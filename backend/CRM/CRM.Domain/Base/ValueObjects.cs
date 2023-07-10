namespace CRM.Domain.Base
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, right) || left.Equals(right);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            return obj is ValueObject other && ValuesAreEqual(other);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        private bool ValuesAreEqual(ValueObject other)
        {
            return other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
    }
}
