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

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].Winner(original, ind);
            }
        }
    }

    public WinnerGameRule<T> Clone()
    {
        return new WinnerGameRule<T>(this.Actions, this.Condition);
    }
}