using System.Collections;

namespace Table;

public class Token<T> : IEnumerable<T>, ICloneable<Token<T>>
{
    private readonly T[] _values;

    public Token(T[] values)
    {
        _values = values;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _values.Length) throw new IndexOutOfRangeException();
            return _values[index];
        }
    }

    public int CantValues => _values.Length;

    /// <summary>Devuelve una copia de la ficha</summary>
    /// <returns>Nueva ficha</returns>
    public Token<T> Clone()
    {
        var values = new T[_values.Length];
        Array.Copy(_values, values, values.Length);
        return new Token<T>(values);
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _values) yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override bool Equals(object? obj)
    {
        var token = (obj as Token<T>)!;
        for (var i = 0; i < token._values.Length; i++)
            if (!_values[i]!.Equals(token._values[i]))
                return false;

        return true;
    }

    public override int GetHashCode()
    {
        return _values[0]!.GetHashCode();
    }

    public override string ToString()
    {
        return "[" + string.Join(' ', _values) + "]";
    }
}