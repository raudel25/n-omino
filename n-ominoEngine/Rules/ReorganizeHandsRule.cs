using InfoGame;
using Table;

namespace Rules;

public class ReorganizeHandsRule<T> : ActionConditionRule<IReorganizeHands<T>, T>, ICloneable<ReorganizeHandsRule<T>>
{
    public ReorganizeHandsRule(IEnumerable<IReorganizeHands<T>> rules, IEnumerable<ICondition<T>> condition,
        IReorganizeHands<T> rule) : base(rules, condition, rule)
    {
    }

    public ReorganizeHandsRule<T> Clone()
    {
        return new ReorganizeHandsRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].Reorganize(tournament, original);
                activate = true;
            }

        if (!activate) Default!.Reorganize(tournament, original);
    }
}