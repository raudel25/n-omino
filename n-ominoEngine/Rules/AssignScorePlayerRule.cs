using InfoGame;
using Table;

namespace Rules;

public class AssignScorePlayerRule<T> : ActionConditionRule<IAssignScorePlayer<T>, T> where T : ICloneable<T>
{
    public AssignScorePlayerRule(IEnumerable<IAssignScorePlayer<T>> rules, IEnumerable<ICondition<T>> condition) : base(
        rules,
        condition, null)
    {
    }

    public override void RunRule(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind)
    {
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(game, ind))
            {
                this.Actions[i].AssignScore(game, rules, ind);
            }
        }
    }

    public AssignScorePlayerRule<T> Clone()
    {
        return new AssignScorePlayerRule<T>(this.Actions, this.Condition);
    }
}