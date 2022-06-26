using InfoGame;
using Table;

namespace Rules;

public class WinnerGameRule<T> : ActionConditionRule<IWinnerGame<T>, T> where T : ICloneable<T>
{
    public WinnerGameRule(IEnumerable<IWinnerGame<T>> rules, IEnumerable<ICondition<T>> condition) : base(rules,
        condition,
        null)
    {
    }

    public override void RunRule(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(game, ind))
            {
                this.Actions[i].Winner(game, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.Winner(game, ind);
    }

    public WinnerGameRule<T> Clone()
    {
        return new WinnerGameRule<T>(this.Actions, this.Condition);
    }
}