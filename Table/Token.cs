namespace Table;

public class Token
{
    public readonly int[] Values;
    public Token(int[] values)
    {
        this.Values = values;
    }
    /// <summary>Devuelve una copia de la ficha</summary>
    /// <returns>Nueva ficha</returns>
    public Token Clone()
    {
        int[] values = new int[this.Values.Length];
        Array.Copy(this.Values, values, values.Length);
        return new Token(values);
    }
    public override bool Equals(object? obj)
    {
        Token token = (obj as Token)!;
        for (int i = 0; i < token.Values.Length; i++)
        {
            if (token.Values[i] != this.Values[i]) return false;
        }
        return true;
    }
    public override int GetHashCode()
    {
        return this.Values[0].GetHashCode();
    }
}
