namespace Rules;

using InfoGame;
using Table;

public class ToPassTokenRule<T> : ActionConditionRule<IToPassToken, T>, ICloneable<ToPassTokenRule<T>>
{
    public bool PossibleToPass { get; private set; }

    public ToPassTokenRule(IEnumerable<IToPassToken> rules, IEnumerable<ICondition<T>> condition,
        IToPassToken rule) : base(rules, condition, rule)
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