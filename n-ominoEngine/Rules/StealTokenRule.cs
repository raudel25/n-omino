using InfoGame;
using Table;

namespace Rules;

public class StealTokenRule<T> : ActionConditionRule<IStealToken<T>, T>, ICloneable<StealTokenRule<T>>
{
    public StealTokenRule(IEnumerable<IStealToken<T>> rules, IEnumerable<ICondition<T>> condition,
        IStealToken<T> rule) : base(
        rules, condition, rule)
    {
    }

    /// <summary>
    ///     Cantidad maxima de fichas a robar
    /// </summary>
    public int CantMax { get; private set; }

    public bool Play { get; private set; }

    public StealTokenRule<T> Clone()
    {
        return new StealTokenRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        IsValidRule<T> valid, IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        var play = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].Steal(game, original, valid, ind, ref play);
                CantMax = Actions[i].CantMax;
                activate = true;
            }

        if (!activate)
        {
            Default!.Steal(game, original, valid, ind, ref play);
            CantMax = Default!.CantMax;
        }

        Play = play;
    }
}