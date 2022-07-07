using Table;

namespace InfoGame;

public class Move<T> : ICloneable<Move<T>>
{
    //ficha que quiero jugar
    public Token<T>? Token;

    //nodo por el que lo quiero jugar
    public INode<T>? Node;

    //índice de la regla que valida la jugada
    public int ValidPlay;

    public Move(Token<T>? token, INode<T>? node, int validPlay)
    {
        this.Token = token;
        this.Node = node;
        this.ValidPlay = validPlay;
    }

    public bool IsAPass()
    {
        //determina si la jugada es un pase
        return this.Token is null && this.Node is null && this.ValidPlay < 0;
    }

    public bool Mata (T value)
    {
        //busco por los nodos si el valor T está en algún "no padre" retorno true
        foreach(var connection in this.Node!.Connections)
        {
            if(connection is null || !this.Node.Fathers.Contains(connection)) continue;
            //if(comp!.Compare(connection.ValueToken[i], value)) return true;
        }
        return false;
    }

    public Move<T> Clone()
    {
        if (this.ValidPlay == -1) return new Move<T>(Token, Node, ValidPlay);
        return new Move<T>(Token!.Clone(), Node, ValidPlay); //hacer nodo ICloneable
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}