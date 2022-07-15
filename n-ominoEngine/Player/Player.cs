using Rules;
using InfoGame;

namespace Player;

public class Player<T>
{
    /// <summary>
    /// Id del jugador en el torneo
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// Tipos de estrategia disponibles
    /// </summary>
    public IStrategy<T>[] Strategies { get; protected set; }

    /// <summary>
    /// Condiciones bajo los cuales se puede jugar esa estrategia
    /// </summary>
    public ICondition<T>[] Conditions { get; protected set; }

    /// <summary>
    /// Estrategia que se ejecuta por defecto
    /// </summary>
    public IStrategy<T> Default { get; }

    private Random random;

    private Scorer<T>.MoveScorer _moveScorer;

    public Player(IEnumerable<IStrategy<T>> strategies, 
                IEnumerable<ICondition<T>> condition, 
                IStrategy<T> def, 
                int id, 
                Scorer<T>.MoveScorer moveScorer)
    {
        this._moveScorer = moveScorer;
        this.Strategies = strategies.ToArray();
        this.Conditions = condition.ToArray();
        this.Default = def;
        this.random = new();
        this.Id = id;
    }

    protected IEnumerable<Move<T>> GetValidMoves(Hand<T> myHand, GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        rules.IsValidPlay.RunRule(null!, status, status, rules, Id);
        foreach (var freNode in status.Table.FreeNode)
        {
            foreach (var token in myHand)
            {
                var validMoves = rules.IsValidPlay.ValidPlays(freNode, token, status, ind);
                foreach (var move in validMoves)
                {
                    yield return new Move<T>(token, freNode, move);

                }
            }
        }
    }
    public virtual Move<T> Play(GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        var myHand = status.Players[status.FindPLayerById(Id)].Hand;
        var validMoves = GetValidMoves(myHand, status, rules, ind);
        var strategiesMoves = GetStrategiesMoves(validMoves, status,rules, Id);
        var move = validMoves.MaxBy(x => this._moveScorer(x, strategiesMoves, status, rules, this.random, this.Id));
        if(move!.IsAPass()) return Default.Play(validMoves,status,rules,Id).First();
        return move;
    }

    protected IEnumerable<IEnumerable<Move<T>>> GetStrategiesMoves(IEnumerable<Move<T>> validMoves,GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        for (int i = 0; i < Conditions.Length; i++)
        {
            //si la condición no está activa, continúo
            if (!Conditions[i].RunRule(null!, status, rules, Id)) continue;
            yield return this.Strategies[i].Play(validMoves, status, rules, Id);
        }
    }
}