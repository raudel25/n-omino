using InfoGame;

namespace Rules;

public class WinnerGameRule : ActionConditionRule<IWinnerGame>
{
    public WinnerGameRule(IEnumerable<IWinnerGame> rules, IEnumerable<ICondition> condition) : base(rules, condition,
        null)
    {
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
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

    public WinnerGameRule Clone()
    {
        return new WinnerGameRule(this.Actions, this.Condition);
    }
}