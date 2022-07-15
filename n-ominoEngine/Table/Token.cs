using System.Collections;

namespace Table;

public class Token<T> : IEnumerable<T>, ICloneable<Token<T>>
{
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in this._values)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this._values.Length) throw new IndexOutOfRangeException();
            return this._values[index];
        }
    }

    public int CantValues
    {
        get { return this._values.Length; }
    }

    private readonly T[] _values;

    public Token(T[] values)
    {
        this._values = values;
    }

    /// <summary>Devuelve una copia de la ficha</summary>
    /// <returns>Nueva ficha</returns>
    public Token<T> Clone()
    {
        T[] values = new T[this._values.Length];
        Array.Copy(this._values, values, values.Length);
        return new Token<T>(values);
    }

    public override bool Equals(object? obj)
    {
        Token<T> token = (obj as Token<T>)!;
        for (int i = 0; i < token._values.Length; i++)
        {
            if (!this._values[i]!.Equals(token._values[i])) return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return this._values[0]!.GetHashCode();
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }

    public override string ToString()
    {
        return "[" + string.Join(' ',_values) + "]";
    }
}