using InfoGame;
using Table;

namespace Rules;

public class TeamsGameRule<T> : ActionConditionRule<ITeamsGame, T>, ICloneable<TeamsGameRule<T>>
{
    public TeamsGameRule(IEnumerable<ITeamsGame> rules, IEnumerable<ICondition<T>> condition,
        ITeamsGame rule) : base(rules, condition, rule)
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
                this.Actions[i].DeterminateTeams(tournament);
                activate = true;
            }
        }

        if (!activate) this.Default!.DeterminateTeams(tournament);
    }

    public TeamsGameRule<T> Clone()
    {
        return new TeamsGameRule<T>(this.Actions, this.Condition, this.Default!);
    }
}