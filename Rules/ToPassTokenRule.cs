namespace Rules;

using InfoGame;

public class ToPassTokenRule : ActionConditionRule<IToPassToken>
{
    public bool PossibleToPass { get; private set; }

    public ToPassTokenRule(IEnumerable<IToPassToken> rules, IEnumerable<ICondition> condition,
        IToPassToken rule) : base(rules, condition, rule)
    {
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(game, ind))
            {
                this.PossibleToPass = this.PossibleToPass || this.Actions[i].ToPass();
                activate = true;
            }
        }

        if (!activate) this.PossibleToPass = this.PossibleToPass || this.Default!.ToPass();
    }

    public ToPassTokenRule Clone()
    {
        return new ToPassTokenRule(this.Actions, this.Condition, this.Default!);
    }
}