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

    public DistributionPlayerRule<T> Clone()
    {
        return new DistributionPlayerRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].DeterminateDistribution(tournament);
                activate = true;
            }

        if (!activate) Default!.DeterminateDistribution(tournament);
    }
}