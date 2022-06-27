using InfoGame;
using Table;

namespace Rules;

public class WinnerGameRule<T> : ActionConditionRule<IWinnerGame<T>, T>, ICloneable<WinnerGameRule<T>> where T : struct
{
    public WinnerGameRule(IEnumerable<IWinnerGame<T>> rules, IEnumerable<ICondition<T>> condition) : base(rules,
        condition,
        null)
    {
    }

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind)
    {
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, ind))
            {
                this.Actions[i].Winner(original, ind);
            }
        }
    }

    public WinnerGameRule<T> Clone()
    {
        return new WinnerGameRule<T>(this.Actions, this.Condition);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}