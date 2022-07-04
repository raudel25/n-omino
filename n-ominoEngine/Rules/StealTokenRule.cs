using InfoGame;
using Table;

namespace Rules;

public class StealTokenRule<T> : ActionConditionRule<IStealToken<T>, T>, ICloneable<StealTokenRule<T>>
{
    /// <summary>
    /// Cantidad maxima de fichas a robar
    /// </summary>
    public int CantMax { get; private set; }

    public bool Play { get; private set; }

    public StealTokenRule(IEnumerable<IStealToken<T>> rules, IEnumerable<ICondition<T>> condition,
        IStealToken<T> rule) : base(
        rules, condition, rule)
    {
    }

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        InfoRules<T> rules, int ind)
    {
        bool activate = false;
        bool play = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
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

    public StealTokenRule<T> Clone()
    {
        return new StealTokenRule<T>(this.Actions, this.Condition, this.Default!);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}