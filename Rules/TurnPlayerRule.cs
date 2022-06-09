using InfoGame;

namespace Rules;

public class TurnPlayerRule : ActionConditionRule<ITurnPlayer>
{
    public TurnPlayerRule(IEnumerable<ITurnPlayer> rules, IEnumerable<ICondition> condition, ITurnPlayer rule) : base(
        rules, condition, rule)
    {
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Critery.Length; i++)
        {
            if (this.Critery[i].RunRule(game, ind))
            {
                this.Actions[i].Turn(game.Turns, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.Turn(game.Turns, ind);
    }

    public TurnPlayerRule Clone()
    {
        return new TurnPlayerRule(this.Actions, this.Critery, this.Default!);
    }
}