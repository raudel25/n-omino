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

    public BeginGameRule<T> Clone()
    {
        return new BeginGameRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].Start(tournament, original);
                activate = true;
            }

        if (!activate) Default!.Start(tournament, original);
    }
}