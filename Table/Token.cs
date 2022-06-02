namespace Table;

public class Token
{
    public int[] Values { get; private set; }
    public int CantValues { get { return Values.Count(); } }
    public int Score { get { return Values.Sum(); } }
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
}
