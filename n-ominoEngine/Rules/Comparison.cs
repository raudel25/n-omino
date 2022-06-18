namespace Rules;

public interface IComparison<T>
{
    /// <summary>Criterio de comparacion</summary>
    /// <param name="a">Entero a comparar</param>
    /// <param name="b">Entero a comparar</param>
    /// <returns>Si el criterio es valido</returns>
    public bool Compare(T a, T b);
}

public class ClassicComparison<T> : IComparison<T>
{
    public bool Compare(T a, T b)
    {
        if (a == null) return false;
        return a.Equals(b);
    }
}