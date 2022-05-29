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
}
