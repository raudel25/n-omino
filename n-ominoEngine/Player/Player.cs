using Table;
using Rules;
using InfoGame;

namespace Player;

//El namespace tiene el mismo nombre que una clase abstracta
public abstract class Player<T>
{
    public readonly int ID;
    IStrategy<Token<T>>[] _plays; //estrategias de juego
    protected IStrategy<Token<T>> _strategy;

    public Player(int id, IStrategy<Token<T>> strategy)
    {
        this.ID = id;
        this._strategy = strategy;
    }

    public abstract (Token<T>, INode<T>) Play(GameStatus<T> status, InfoRules<T> rules);

    public Dictionary<Token<T>, List<INode<T>>> GetPossiblePlay(TableGame<T> table, IEnumerable<Token<T>> hand,
        InfoRules<T> rules)
    {
        Dictionary<Token<T>, List<INode<T>>> possible = new();
        foreach (var token in hand)
        {
            foreach (var node in table.FreeNode)
            {
                //Consultar con anabel           //if(!rules.ValidPlay.ValidPlay(node,token,table)) continue;
                if (!possible.ContainsKey(token))
                {
                    List<INode<T>> nodes = new List<INode<T>>();
                    nodes.Add(node);
                    possible.Add(token, nodes);
                }
                else possible[token].Add(node);
            }
        }
        return possible;
    }
}

public class XPlayer<T> : Player<T>
{
    public XPlayer(int id, IStrategy<Token<T>> strategy) : base(id, strategy)
    {
    }

    public override (Token<T>, INode<T>) Play(GameStatus<T> status, InfoRules<T> rules)
    {
        IEnumerable<Token<T>> MyHand = status.Players[ID].Hand;
        Dictionary<Token<T>, List<INode<T>>> Posibble = this.GetPossiblePlay(status.Table, MyHand, rules);
        Token<T> TokenToPlay = this._strategy.Play(Posibble.Keys);
        INode<T> node = Posibble[TokenToPlay][0];
        return (TokenToPlay, node);
        // while(MyHand.Count > 0)
        // {
        //     Token token = play.Play();
        //     foreach (var node in status.Table.FreeNode)
        //         if(rules.ValidPlay.ValidPlay(node, token, status.Table)) return(token, node);
        //     MyHand.Remove(token);
        // }
        // return(null!,null!);
    }
}

public class RandomPlayer<T> : IStrategy<T>
{
    public RandomPlayer() { }
    public T Play(IEnumerable<T> tokens)
    {
        Random r = new Random();
        int index = r.Next(0, tokens.Count());
        return tokens.ElementAt(index);
    }
}


public class BotaGordaPlayer<T> : IStrategy<T>
{
    IComparer<T> _comparer;
    public BotaGordaPlayer(IComparer<T> comparer)
    {
        this._comparer = comparer;
    }
    public T Play(IEnumerable<T> tokens)
    {
        tokens = tokens.OrderByDescending(x => x, _comparer);
        return tokens.ElementAt(0);
    }
}

public interface IStrategy<T>
{
    public T Play(IEnumerable<T> tokens);
}