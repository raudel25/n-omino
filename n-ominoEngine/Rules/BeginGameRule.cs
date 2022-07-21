using InfoGame;
using Table;

namespace Rules;

public class BeginGameRule<T> : ActionConditionRule<IBeginGame<T>, T>, ICloneable<BeginGameRule<T>>
{
    public BeginGameRule(IEnumerable<IBeginGame<T>> rules, IEnumerable<ICondition<T>> condition,
        IBeginGame<T> rule) : base(
        rules,
        condition, rule)
    {
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].Start(tournament, original);
                activate = true;
            }
        }

        if (!activate) this.Default!.Start(tournament, original);
    }

    public BeginGameRule<T> Clone()
    {
        return new BeginGameRule<T>(this.Actions, this.Condition, this.Default!);
    }
}