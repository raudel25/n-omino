using InfoGame;
using Table;

namespace Rules;

public class TurnPlayerRule<T> : ActionConditionRule<ITurnPlayer, T>, ICloneable<TurnPlayerRule<T>>
{
    public TurnPlayerRule(IEnumerable<ITurnPlayer> rules, IEnumerable<ICondition<T>> condition, ITurnPlayer rule) :
        base(
            rules, condition, rule)
    {
    }

    public TurnPlayerRule<T> Clone()
    {
        return new TurnPlayerRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].Turn(original.Turns, ind);
                activate = true;
            }

        if (!activate) Default!.Turn(original.Turns, ind);
    }
}