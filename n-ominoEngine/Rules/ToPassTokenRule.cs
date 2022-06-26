namespace Rules;

using InfoGame;
using Table;

public class ToPassTokenRule<T> : ActionConditionRule<IToPassToken, T> where T : struct
{
    public bool PossibleToPass { get; private set; }

    public ToPassTokenRule(IEnumerable<IToPassToken> rules, IEnumerable<ICondition<T>> condition,
        IToPassToken rule) : base(rules, condition, rule)
    {
    }

    public override void RunRule(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(original, ind))
            {
                this.PossibleToPass = this.PossibleToPass || this.Actions[i].ToPass();
                activate = true;
            }
        }

        if (!activate) this.PossibleToPass = this.PossibleToPass || this.Default!.ToPass();
    }

    public ToPassTokenRule<T> Clone()
    {
        return new ToPassTokenRule<T>(this.Actions, this.Condition, this.Default!);
    }
}