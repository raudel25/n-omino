using Table;

namespace Rules;

public class ComodinTokenDimension : IValidPlay
{
    private Token _comodinToken { get; set; }
    public ComodinTokenDimension(Token token)
    {
        this._comodinToken = token;
    }
    public bool ValidPlay(Node node, Token token, TableGame table)
    {
        return _comodinToken.Equals(token);
    }
    public int[] AsignValues(Node node, Token token, TableGame table)
    {
        return this._comodinToken.Values.ToArray();
    }
}
