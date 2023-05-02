using InfoGame;
using Table;

namespace Rules;

public class WinnerGameRule<T> : ActionConditionRule<IWinnerGame<T>, T>, ICloneable<WinnerGameRule<T>>
{
    public WinnerGameRule(IEnumerable<IWinnerGame<T>> rules, IEnumerable<ICondition<T>> condition) : base(rules,
        condition,
        null)
    {
    }

    public WinnerGameRule<T> Clone()
    {
        return new WinnerGameRule<T>(Actions, Condition);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
                Actions[i].Winner(original, ind);
    }
}