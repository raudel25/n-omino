using InfoGame;

namespace Rules;

public class VisibilityPlayerRule : ActionConditionRule<IVisibilityPlayer>
{
    public VisibilityPlayerRule(IEnumerable<IVisibilityPlayer> rules, IEnumerable<ICondition> condition,
        IVisibilityPlayer rule) : base(rules, condition, rule)
    {
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(game, ind))
            {
                this.Actions[i].Visibility(game, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.Visibility(game, ind);
    }

    public VisibilityPlayerRule Clone()
    {
        return new VisibilityPlayerRule(this.Actions, this.Condition, this.Default!);
    }
}