using InfoGame;
using Table;

namespace Rules;

public class ScorePlayerTournamentRule<T> : ActionConditionRule<IScorePlayerTournament<T>, T>,
    ICloneable<ScorePlayerTournamentRule<T>>
{
    public ScorePlayerTournamentRule(IEnumerable<IScorePlayerTournament<T>> rules, IEnumerable<ICondition<T>> condition,
        IScorePlayerTournament<T> rule) : base(rules, condition, rule)
    {
    }

    public ScorePlayerTournamentRule<T> Clone()
    {
        return new ScorePlayerTournamentRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].AssignScore(tournament, game);
                activate = true;
            }

        if (!activate) Default!.AssignScore(tournament, game);
    }
}