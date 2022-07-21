using InfoGame;
using Table;

namespace Rules;

public class ReorganizeHandsRule<T> : ActionConditionRule<IReorganizeHands<T>, T>, ICloneable<ReorganizeHandsRule<T>>
{
    public ReorganizeHandsRule(IEnumerable<IReorganizeHands<T>> rules, IEnumerable<ICondition<T>> condition,
        IReorganizeHands<T> rule) : base(rules, condition, rule)
    {
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].Reorganize(tournament, original);
                activate = true;
            }
        }

        if (!activate) this.Default!.Reorganize(tournament, original);
    }

    public ReorganizeHandsRule<T> Clone()
    {
        return new ReorganizeHandsRule<T>(this.Actions, this.Condition, this.Default!);
    }
}