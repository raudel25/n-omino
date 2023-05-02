using InfoGame;
using Table;

namespace Rules;

public class AssignScorePlayerRule<T> : ActionConditionRule<IAssignScorePlayer<T>, T>,
    ICloneable<AssignScorePlayerRule<T>>
{
    public AssignScorePlayerRule(IEnumerable<IAssignScorePlayer<T>> rules, IEnumerable<ICondition<T>> condition) : base(
        rules,
        condition, null)
    {
    }

    public AssignScorePlayerRule<T> Clone()
    {
        return new AssignScorePlayerRule<T>(Actions, Condition);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
                Actions[i].AssignScore(original, rules, ind);
    }
}