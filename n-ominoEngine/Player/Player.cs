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

    //Devuelve las jugadas válidas que puedo hacer
    protected IEnumerable<Move<T>> GetValidMoves(Hand<T> myHand, TournamentStatus tournament, GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        rules.IsValidPlay.RunRule(tournament, status, status, rules, Id);
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

    //Devuelve la jugada que va a hacer el player
    public virtual Move<T> Play(TournamentStatus tournamnet, GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        var myHand = status.Players[status.FindPLayerById(Id)].Hand;
        var validMoves = GetValidMoves(myHand, tournamnet, status, rules, ind);
        //obtengo todas las estrategias
        var strategiesMoves = GetStrategiesMoves(validMoves, tournamnet, status, rules, Id);
        //me quedo con la de máxima puntuación según el scorer
        var move = validMoves.MaxBy(x => this._moveScorer(x, strategiesMoves, status, rules, this.random, this.Id));
        //si lo que tenía era un pase, me quedo con la estrategia del default
        if (move!.IsAPass()) return Default.Play(validMoves, status, rules, Id).First();
        return move;
    }
    
    protected IEnumerable<IEnumerable<Move<T>>> GetStrategiesMoves(IEnumerable<Move<T>> validMoves, TournamentStatus tournament, GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        for (int i = 0; i < Conditions.Length; i++)
        {
            //si la condición no está activa, continúo
            if (!Conditions[i].RunRule(tournament, status, rules, Id)) continue;
            yield return this.Strategies[i].Play(validMoves, status, rules, Id);
        }
    }
}