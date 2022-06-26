using InfoGame;
using Table;

namespace Rules;

public class TurnPlayerRule<T> : ActionConditionRule<ITurnPlayer, T> where T : struct
{
    public TurnPlayerRule(IEnumerable<ITurnPlayer> rules, IEnumerable<ICondition<T>> condition, ITurnPlayer rule) :
        base(
            rules, condition, rule)
    {
    }

    public override void RunRule(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(original, ind))
            {
                this.Actions[i].Turn(original.Turns, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.Turn(original.Turns, ind);
    }

    public TurnPlayerRule<T> Clone()
    {
        return new TurnPlayerRule<T>(this.Actions, this.Condition, this.Default!);
    }
}