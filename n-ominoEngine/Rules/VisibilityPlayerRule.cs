using InfoGame;
using Table;

namespace Rules;

public class VisibilityPlayerRule<T> : ActionConditionRule<IVisibilityPlayer<T>, T>, ICloneable<VisibilityPlayerRule<T>>
{
    public VisibilityPlayerRule(IEnumerable<IVisibilityPlayer<T>> rules, IEnumerable<ICondition<T>> condition,
        IVisibilityPlayer<T> rule) : base(rules, condition, rule)
    {
    }

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].Visibility(game, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.Visibility(game, ind);
    }

    public VisibilityPlayerRule<T> Clone()
    {
        return new VisibilityPlayerRule<T>(this.Actions, this.Condition, this.Default!);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}