using Table;

namespace InfoGame;

public class Jugada<T> : ICloneable<Jugada<T>>
{
    //ficha que quiero jugar
    public Token<T> Token;
    //nodo por el que lo quiero jugar
    public INode<T> Node;
    //índice de la regla que valida la jugada
    public int ValidPlay;
    public Jugada(Token<T> token, INode<T> node, int validPlay)
    {
        this.Token = token;
        this.Node = node;
        this.ValidPlay = validPlay;
    }
    public Jugada<T> Clone()
    {
        return new Jugada<T>(Token.Clone(), Node, ValidPlay); //hacer nodo ICloneable
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}