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

    public void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].AssignScore(tournament, game);
                activate = true;
            }
        }

        if (!activate) this.Default!.AssignScore(tournament, game);
    }

    public ScorePlayerTournamentRule<T> Clone()
    {
        return new ScorePlayerTournamentRule<T>(this.Actions, this.Condition, this.Default!);
    }
}