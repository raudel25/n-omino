using InfoGame;
using Table;

namespace Rules;

public class WinnerTournamentRule<T> : ActionConditionRule<IWinnerTournament, T>, ICloneable<WinnerTournamentRule<T>>
{
    public WinnerTournamentRule(IEnumerable<IWinnerTournament> rules, IEnumerable<ICondition<T>> condition) : base(rules, condition, null)
    {
    }

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        InfoRules<T> rules, int ind)
    {
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].DeterminateWinner(tournament, ind);
            }
        }
    }

    public WinnerTournamentRule<T> Clone()
    {
        return new WinnerTournamentRule<T>(this.Actions, this.Condition);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}