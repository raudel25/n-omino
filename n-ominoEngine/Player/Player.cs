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
    //va a jugar con esta estrategia siempre que sea posible
    IStrategy<T> _strategy { get; set; }
    //si no lo es juega con esta
    IStrategy<T> _default { get; set; }
    public PurePlayer(int id, IStrategy<T> strategy, IStrategy<T> def) : base(id)
    {
        this._strategy = strategy;
        this._default = def;
    }
    public override Jugada<T> Play(GameStatus<T> status, InfoRules<T> rules)
    {
        var possibleJugadas = this.GetValidJugadas(status.Players[Id].Hand!, status, rules);
        int index = _strategy.Play(possibleJugadas, status, rules, Id);
        if(index == -1) index = _default.Play(possibleJugadas, status, rules, Id);
        return possibleJugadas[index];
    }
}


public abstract class ConditionPlayer<T>: Player<T> where T :struct 
{
    /// <summary>
    /// Acciones que determinan las reglas
    /// </summary>
    public IStrategy<T>[] Strategies { get; protected set; }

    /// <summary>
    /// Criterios bajo los cuales se ejecutan las reglas
    /// </summary>
    public ICondition<T>[] Conditions { get; protected set; }

    /// <summary>
    /// Regla que se ejecuta por defecto
    /// </summary>
    public IStrategy<T> Default { get; }

    public ConditionPlayer(IEnumerable<IStrategy<T>> strategies, IEnumerable<ICondition<T>> condition, IStrategy<T> def, int Id) : base(Id)
    {
        this.Strategies = strategies.ToArray();
        this.Conditions = condition.ToArray();
        this.Default = def;
    }

    /// <summary>
    /// Determinar la regla a utilizar
    /// </summary>
    /// <param name="game">Estado del juego clonado</param>
    /// <param name="original">Estado del juego original</param>
    /// <param name="rules">Reglas</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public abstract override Jugada<T> Play(GameStatus<T> status, InfoRules<T> rules);
}

//Evalúa y elige la mejor estrategia para jugar
public class EvaluatePlayer<T> : ConditionPlayer<T> where T : struct 
{
    Func<Jugada<T>,GameStatus<T>,InfoRules<T>, int> _moveScorer;
    public EvaluatePlayer(IEnumerable<IStrategy<T>> strategies, IEnumerable<ICondition<T>> condition, IStrategy<T> def, int Id, Func<Jugada<T>,GameStatus<T>,InfoRules<T>, int> moveScorer) : base(strategies,condition,def,Id)
    {
        this._moveScorer = moveScorer;
    }
    public override Jugada<T> Play(GameStatus<T> status, InfoRules<T> rules)
    {
        var myHand = status.Players[Id].Hand;
        var validMoves = GetValidJugadas(myHand, status, rules);
        int index = this.Default.Play(validMoves,status,rules,Id);
        for (int i = 0; i < Conditions.Length; i++)
        {
            //si la condición no está activa, continúo
            if(!Conditions[i].RunRule(status,Id)) continue;
            //si lo está compruebo si la estrategia me aporta más que la que tenía
            int possibleMove = Strategies[i].Play(validMoves, status, rules, Id);
            if(_moveScorer(validMoves[possibleMove], status, rules) > _moveScorer(validMoves[index], status, rules))
                index = possibleMove;
        }
        return validMoves[index];
    }
}

// public class RandomStrategyPlayer<T> : ConditionPlayer<T> where T : struct 
// {
    
// }
