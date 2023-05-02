using InfoGame;
using Table;

namespace Rules;

public class VisibilityPlayerRule<T> : ActionConditionRule<IVisibilityPlayer<T>, T>, ICloneable<VisibilityPlayerRule<T>>
{
    public VisibilityPlayerRule(IEnumerable<IVisibilityPlayer<T>> rules, IEnumerable<ICondition<T>> condition,
        IVisibilityPlayer<T> rule) : base(rules, condition, rule)
    {
    }

    public VisibilityPlayerRule<T> Clone()
    {
        return new VisibilityPlayerRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].Visibility(game, ind);
                activate = true;
            }

        if (!activate) Default!.Visibility(game, ind);
    }
}