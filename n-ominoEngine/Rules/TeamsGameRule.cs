using InfoGame;
using Table;

namespace Rules;

public class TeamsGameRule<T> : ActionConditionRule<ITeamsGame, T>, ICloneable<TeamsGameRule<T>>
{
    public TeamsGameRule(IEnumerable<ITeamsGame> rules, IEnumerable<ICondition<T>> condition,
        ITeamsGame rule) : base(rules, condition, rule)
    {
    }

    public TeamsGameRule<T> Clone()
    {
        return new TeamsGameRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].DeterminateTeams(tournament);
                activate = true;
            }

        if (!activate) Default!.DeterminateTeams(tournament);
    }
}