using InfoGame;
using Table;

namespace Rules;

public class DistributionPlayerRule<T> : ActionConditionRule<IDistributionPlayer, T>,
    ICloneable<DistributionPlayerRule<T>>
{
    public DistributionPlayerRule(IEnumerable<IDistributionPlayer> rules, IEnumerable<ICondition<T>> condition,
        IDistributionPlayer rule) : base(rules, condition, rule)
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
                this.Actions[i].DeterminateDistribution(tournament);
                activate = true;
            }
        }

        if (!activate) this.Default!.DeterminateDistribution(tournament);
    }

    public DistributionPlayerRule<T> Clone()
    {
        return new DistributionPlayerRule<T>(this.Actions, this.Condition, this.Default!);
    }
}