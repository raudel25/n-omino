using Table;

namespace InfoGame;

public class Move<T> : ICloneable<Move<T>>
{
    //nodo por el que lo quiero jugar
    public INode<T>? Node;

    //ficha que quiero jugar
    public Token<T>? Token;

    //índice de la regla que valida la jugada
    public int ValidPlay;

    public Move(Token<T>? token, INode<T>? node, int validPlay)
    {
        Token = token;
        Node = node;
        ValidPlay = validPlay;
    }

    public Move<T> Clone()
    {
        if (ValidPlay == -1) return new Move<T>(Token, Node, ValidPlay);
        return new Move<T>(Token!.Clone(), Node, ValidPlay);
    }

    public bool IsAPass()
    {
        //determina si la jugada es un pase
        return Token is null && Node is null && ValidPlay < 0;
    }

    public bool Mata(T value)
    {
        //determina si la jugada se hizo para matar este valor
        //busco por los nodos si el valor T está en algún "no padre" retorno true
        foreach (var connection in Node!.Connections)
        {
            if (connection is null || !Node.Fathers.Contains(connection)) continue;
            if (connection.ValueToken.Contains(value)) return true;
        }

        return false;
    }
}