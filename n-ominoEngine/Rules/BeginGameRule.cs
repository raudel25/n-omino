using InfoGame;
using Table;

namespace Rules;

public class BeginGameRule<T> : ActionConditionRule<IBeginGame<T>, T>, ICloneable<BeginGameRule<T>> where T : struct
{
    public BeginGameRule(IEnumerable<IBeginGame<T>> rules, IEnumerable<ICondition<T>> condition,
        IBeginGame<T> rule) : base(
        rules,
        condition, rule)
    {
    }

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, ind))
            {
                this.Actions[i].Start(tournament, original, rules);
                activate = true;
            }
        }

        if (!activate) this.Default!.Start(tournament, original, rules);
    }

    public BeginGameRule<T> Clone()
    {
        return new BeginGameRule<T>(this.Actions, this.Condition, this.Default!);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}