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

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].AssignScore(tournament, game, rules, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.AssignScore(tournament, game, rules, ind);
    }

    public ScoreTeamTournamentRule<T> Clone()
    {
        return new ScoreTeamTournamentRule<T>(this.Actions, this.Condition, this.Default!);
    }
}