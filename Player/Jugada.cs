using Table;
using Rules;
using InfoGame;

namespace Player;

public class Jugada<T> : ICloneable<Jugada<T>>
{
    public Token<T> Token;
    public INode<T> Node;
    public IValidPlay<T> ValidPlay;
    public Jugada(Token<T> token, INode<T> node, IValidPlay<T> validPlay)
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