using InfoGame;
using Table;

namespace Rules;

public class DistributionPlayerRule<T> : ActionConditionRule<IDistributionPlayer, T>, ICloneable<DistributionPlayerRule<T>>
{
    public DistributionPlayerRule(IEnumerable<IDistributionPlayer> rules, IEnumerable<ICondition<T>> condition,
        IDistributionPlayer rule) : base(rules, condition, rule)
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
                this.Actions[i].DeterminateDistribution(tournament, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.DeterminateDistribution(tournament, ind);
    }

    public DistributionPlayerRule<T> Clone()
    {
        return new DistributionPlayerRule<T>(this.Actions, this.Condition, this.Default!);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}