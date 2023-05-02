using InfoGame;
using Table;

namespace Rules;

public class ToPassTokenRule<T> : ActionConditionRule<IToPassToken, T>, ICloneable<ToPassTokenRule<T>>
{
    public ToPassTokenRule(IEnumerable<IToPassToken> rules, IEnumerable<ICondition<T>> condition,
        IToPassToken rule) : base(rules, condition, rule)
    {
    }

    public bool PossibleToPass { get; private set; }

    public ToPassTokenRule<T> Clone()
    {
        return new ToPassTokenRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                PossibleToPass = PossibleToPass || Actions[i].ToPass();
                activate = true;
            }

        if (!activate) PossibleToPass = PossibleToPass || Default!.ToPass();
    }
}