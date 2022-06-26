using Table;
using Rules;
using InfoGame;

namespace Player;

public abstract class Player<T> where T : struct
{
    public readonly int Id;
    public Player(int id)
    {
        this.Id = id;
    }
    public abstract Jugada<T> Play(GameStatus<T> status, InfoRules<T> rules);
    protected List<Jugada<T>> GetValidJugadas(Hand<T> myHand, GameStatus<T> status, InfoRules<T> rules)
    {
        var res = new List<Jugada<T>>();
        foreach (var freNode in status.Table.FreeNode)
        {
            foreach (var token in myHand)
            {
                var validmoves = rules.IsValidPlay.ValidPlays(freNode, token, status.Table);
                for (int i = 0; i < validmoves.Count; i++)
                {
                    res.Add(new Jugada<T>(token, freNode, validmoves[i]));
                }
            }
        }
        return res;
    }
}

public class PurePlayer<T> : Player<T> where T : struct
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
