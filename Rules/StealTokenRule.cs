using InfoGame;

namespace Rules;

public class StealTokenRule : ActionConditionRule<IStealToken>
{
    /// <summary>
    /// Cantidad maxima de fichas a robar
    /// </summary>
    public int CantMax { get; private set; }
    public bool Play { get; private set; }

    public StealTokenRule(IEnumerable<IStealToken> rules, IEnumerable<ICondition> condition, IStealToken rule) : base(
        rules, condition, rule)
    {
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
    {
        bool activate = false;
        bool play = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(game, ind))
            {
                this.Actions[i].Steal(game, original, rules, ind, ref play);
                this.CantMax = this.Actions[i].CantMax;
                activate = true;
            }
        }

        if (!activate)
        {
            this.Default!.Steal(game, original, rules, ind, ref play);
            this.CantMax = this.Default.CantMax;
        }

        this.Play = play;
    }

    public StealTokenRule Clone()
    {
        return new StealTokenRule(this.Actions, this.Condition, this.Default!);
    }
}