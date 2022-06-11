namespace Table;

public class Token<T>
{
    public readonly T[] Values;

    public Token(T[] values)
    {
        this.Values = values;
    }

    /// <summary>Devuelve una copia de la ficha</summary>
    /// <returns>Nueva ficha</returns>
    public Token<T> Clone()
    {
        T[] values = new T[this.Values.Length];
        Array.Copy(this.Values, values, values.Length);
        return new Token<T>(values);
    }

    public override bool Equals(object? obj)
    {
        Token<T> token = (obj as Token<T>)!;
        for (int i = 0; i < token.Values.Length; i++)
        {
            if (!this.Values[i].Equals(token.Values[i])) return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return this.Values[0].GetHashCode();
    }
}