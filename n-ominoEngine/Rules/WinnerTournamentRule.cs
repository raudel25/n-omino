using InfoGame;
using Table;

namespace Rules;

public class WinnerTournamentRule<T> : ActionConditionRule<IWinnerTournament, T>, ICloneable<WinnerTournamentRule<T>>
{
    public WinnerTournamentRule(IEnumerable<IWinnerTournament> rules, IEnumerable<ICondition<T>> condition) : base(
        rules, condition, null)
    {
    }

    public WinnerTournamentRule<T> Clone()
    {
        return new WinnerTournamentRule<T>(Actions, Condition);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
                Actions[i].DeterminateWinner(tournament);
    }
}