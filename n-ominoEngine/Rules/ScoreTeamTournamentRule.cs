using InfoGame;
using Table;

namespace Rules;

public class ScoreTeamTournamentRule<T> : ActionConditionRule<IScoreTeamTournament<T>, T>,
    ICloneable<ScoreTeamTournamentRule<T>>
{
    public ScoreTeamTournamentRule(IEnumerable<IScoreTeamTournament<T>> rules, IEnumerable<ICondition<T>> condition,
        IScoreTeamTournament<T> rule) : base(rules, condition, rule)
    {
    }

    public ScoreTeamTournamentRule<T> Clone()
    {
        return new ScoreTeamTournamentRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].AssignScore(tournament, original, rules);
                activate = true;
            }

        if (!activate) Default!.AssignScore(tournament, original, rules);
    }
}