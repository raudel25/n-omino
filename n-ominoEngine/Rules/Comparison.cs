namespace Rules;

public class ClassicComparison<T> : IComparison<T>
{
    public bool Compare(T a, T b)
    {
        if (a == null) return false;
        return a.Equals(b);
    }
}