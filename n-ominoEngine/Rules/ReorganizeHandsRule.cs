using InfoGame;
using Table;

namespace Rules;

public class ReorganizeHandsRule<T> : ActionConditionRule<IReorganizeHands<T>, T>, ICloneable<ReorganizeHandsRule<T>>
{
    public ReorganizeHandsRule(IEnumerable<IReorganizeHands<T>> rules, IEnumerable<ICondition<T>> condition,
        IReorganizeHands<T> rule) : base(rules, condition, rule)
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
                this.Actions[i].Reorganize(tournament, game, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.Reorganize(tournament, game, ind);
    }

    public ReorganizeHandsRule<T> Clone()
    {
        return new ReorganizeHandsRule<T>(this.Actions, this.Condition, this.Default!);
    }
}