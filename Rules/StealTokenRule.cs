using InfoGame;

namespace Rules;

public class StealTokenRule : ActionConditionRule<IStealToken>
{
    public int CantMin { get; private set; }
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
        for (int i = 0; i < this.Critery.Length; i++)
        {
            if (this.Critery[i].RunRule(game, ind))
            {
                this.Actions[i].Steal(game, original, rules, ind, ref play);
                this.CantMax = this.Actions[i].CantMax;
                this.CantMin = this.Actions[i].CantMin;
                activate = true;
            }
        }

        if (!activate)
        {
            this.Default!.Steal(game, original, rules, ind, ref play);
            this.CantMax = this.Default.CantMax;
            this.CantMin = this.Default.CantMin;
        }

        this.Play = play;
    }

    public StealTokenRule Clone()
    {
        return new StealTokenRule(this.Actions, this.Critery, this.Default!);
    }
}