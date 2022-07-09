using Table;
using Rules;
using InfoGame;

namespace Player;

public abstract class Player<T>
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
    /// Criterios bajo los cuales se puede jugar esa estrategia
    /// </summary>
    public ICondition<T>[] Conditions { get; protected set; }

    /// <summary>
    /// Estrategia que se ejecuta por defecto
    /// </summary>
    public IStrategy<T> Default { get; }

    protected Random random;

    public Player(IEnumerable<IStrategy<T>> strategies, IEnumerable<ICondition<T>> condition, IStrategy<T> def, int Id)
    {
        this.Strategies = strategies.ToArray();
        this.Conditions = condition.ToArray();
        this.Default = def;
        this.random = new();
        this.Id = Id;
    }

    protected List<Move<T>> GetValidMoves(Hand<T> myHand, GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        rules.IsValidPlay.RunRule(null!, status, status, rules, Id);
        var res = new List<Move<T>>();
        foreach (var freNode in status.Table.FreeNode)
        {
            foreach (var token in myHand)
            {
                var ValidMoves = rules.IsValidPlay.ValidPlays(freNode, token, status, ind);
                foreach (var move in ValidMoves)
                    res.Add(new Move<T>(token, freNode, move));
            }
        }
        return res;
    }
    public abstract Move<T> Play(GameStatus<T> status, InfoRules<T> rules, int ind);
}

public class EvaluatePlayer<T> : Player<T>
{
    
    Func<Move<T>, GameStatus<T>, InfoRules<T>, double> _moveScorer;
    public EvaluatePlayer(IEnumerable<IStrategy<T>> strategies, 
                            IEnumerable<ICondition<T>> condition, 
                            IStrategy<T> def, int Id, 
                            Func<Move<T>, GameStatus<T>, InfoRules<T>, double> moveScorer) 
                            : base(strategies, condition, def, Id)
    {
        this._moveScorer = moveScorer;
    }
    public override Move<T> Play(GameStatus<T> status, InfoRules<T> rules, int ind)
    {
        var myHand = status.Players[status.FindPLayerById(Id)].Hand;
        var validMoves = GetValidMoves(myHand, status, rules, ind);

        int index = this.Default.Play(validMoves, status, rules, Id);

        for (int i = 0; i < Conditions.Length; i++)
        {
            //si la condición no está activa, continúo
            if (!Conditions[i].RunRule(null!, status, rules, Id)) continue;
            //si lo está compruebo si la estrategia me aporta más que la que tenía
            int possibleMove = Strategies[i].Play(validMoves, status, rules, Id);
            if( possibleMove == -1 ) continue;
            if (_moveScorer(validMoves[possibleMove], status, rules) > _moveScorer(validMoves[index], status, rules))
                index = possibleMove;
        }
        
        return validMoves[index];
    }
}

public class RandomStrategyPlayer<T> : Player<T> where T : struct 
{
    public RandomStrategyPlayer(IEnumerable<IStrategy<T>> strategies, 
                            IEnumerable<ICondition<T>> conditions, 
                            IStrategy<T> def, 
                            int Id) : base(strategies, conditions, def, Id){}
    public override Move<T> Play(GameStatus<T> status, InfoRules<T> rules,int ind)
    {
        var myHand = status.Players[status.FindPLayerById(Id)].Hand;
        var validMoves = GetValidMoves(myHand, status, rules, ind);

        List<int> indexes = new();
        for (int i = 0; i < Conditions.Length; i++)
        {
            if(Conditions[i].RunRule(null!, status, rules, Id)) indexes.Add(i);
        }
        
        int moveindex = -1;
        while(moveindex == -1 && indexes.Count != 0)
        {
            int i = random.Next(indexes.Count);
            moveindex = Strategies[i].Play(validMoves, status, rules, Id);
            indexes.Remove(i);
        }
        if(moveindex == -1) moveindex = this.Default.Play(validMoves, status, rules, Id);
        if(moveindex < 0 || moveindex >= validMoves.Count) return validMoves[0];
        return validMoves[moveindex];
    }
}
