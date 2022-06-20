using Table;
using Rules;
using InfoGame;

namespace Player;

public abstract class Player<T>
{
    public readonly int Id;
    public Player(int id)
    {
        this.Id = id;
    }
    public abstract Jugada<T> Play(GameStatus<T> status, InfoRules<T> rules);
    protected List<Jugada<T>> GetValidJugadas(IList<Token<T>> items, GameStatus<T> status, InfoRules<T> rules)
    {
        var res = new List<Jugada<T>>();
        foreach (var item in status.Table.FreeNode)
        {
            foreach (var x in items)
            {
                var validMoves = rules.IsValidPlay.ValidPlays(item, x, status.Table);
                for (int i = 0; i < validMoves.Count; i++)
                {
                    res.Add(new Jugada<T>(x,item,i));
                }
            }
        }
        return res;
    }
}

public class PurePlayer<T> : Player<T>
{
    IStrategy<T> _strategy { get; set; }
    public PurePlayer(int id, IStrategy<T> strategy) : base(id)
    {
        this._strategy = strategy;
    }
    public override Jugada<T> Play(GameStatus<T> status, InfoRules<T> rules)
    {
        var possibleJugadas = this.GetValidJugadas(status.Players[Id].Hand!, status, rules);
        return _strategy.Play(possibleJugadas, status, rules);
    }
}

public interface ICloneable<T> : ICloneable
{
    public T Clone();
}