using Table;
using Rules;
using InfoGame;

namespace Player;

public abstract class Player
{
    public readonly int ID;
    IStrategy<Token>[] _plays; //estrategias de juego
    protected IStrategy<Token> _strategy;
    public Player(int id, IStrategy<Token> strategy)
    {
        this.ID = id;
        this._strategy = strategy;
    }
    public abstract (Token, INode) Play(GameStatus status, InfoRules rules);

    public Dictionary<Token, List<INode>> GetPossiblePlay(TableGame table, IEnumerable<Token> hand, InfoRules rules)
    {
        Dictionary<Token, List<INode>> possible = new();
        foreach (var token in hand)
        {
            foreach (var node in table.FreeNode)
            {
                //Consultar con anabel           //if(!rules.ValidPlay.ValidPlay(node,token,table)) continue;
                if (!possible.ContainsKey(token))
                {
                    List<INode> nodes = new List<INode>();
                    nodes.Add(node);
                    possible.Add(token, nodes);
                }
                else possible[token].Add(node);
            }
        }
        return possible;
    }
}

public class XPlayer : Player
{
    public XPlayer(int id, IStrategy<Token> strategy) : base(id, strategy) { }
    public override (Token, INode) Play(GameStatus status, InfoRules rules)
    {
        IEnumerable<Token> MyHand = status.Players[ID].Hand;
        Dictionary<Token, List<INode>> Posibble = this.GetPossiblePlay(status.Table, MyHand, rules);
        Token TokenToPlay = this._strategy.Play(Posibble.Keys);
        INode node = Posibble[TokenToPlay][0];
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