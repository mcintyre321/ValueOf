namespace ValueOf
{
    public class ValueOf<TValue, TThis> where TThis : ValueOf<TValue, TThis>, new()
    {
        /// <summary>
        /// WARNING - THIS FEATURE IS EXPERIMENTAL. I may change it to do
        /// validation in a different way.
        /// Right now, override this method, and throw any exceptions you need to.
        /// Access this.Value to check the value
        /// </summary>
        protected virtual void Validate()
        {
        }

        protected virtual bool TryValidate()
        {
            return true;
        }

        static ValueOf()
        {
        }

        public TValue Value { get; protected set; }

        public static TThis From(TValue item)
        {
            TThis x = new();
            x.Value = item;
            x.Validate();

            return x;
        }

        public static bool TryFrom(TValue item, out TThis thisValue)
        {
            TThis x = new();
            x.Value = item;

            thisValue = x.TryValidate()
               ? x
               : null;

            return thisValue != null;
        }

        protected virtual bool Equals(ValueOf<TValue, TThis> other)
        {
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((ValueOf<TValue, TThis>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(Value);
        }

        public static bool operator ==(ValueOf<TValue, TThis> a, ValueOf<TValue, TThis> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueOf<TValue, TThis> a, ValueOf<TValue, TThis> b)
        {
            return !(a == b);
        }

        // Implicit operator removed. See issue #14.

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
