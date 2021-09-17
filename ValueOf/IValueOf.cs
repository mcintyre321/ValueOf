namespace ValueOf
{
    public interface IValueOf<TValue, TThis>
    {
        static abstract TThis From(TValue item);
        TValue Value { get; }
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}