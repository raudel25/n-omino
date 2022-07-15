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

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].AssignScore(tournament, game, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.AssignScore(tournament, game, ind);
    }

    public ScorePlayerTournamentRule<T> Clone()
    {
        return new ScorePlayerTournamentRule<T>(this.Actions, this.Condition, this.Default!);
    }
}