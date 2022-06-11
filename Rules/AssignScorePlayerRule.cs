using InfoGame;

namespace Rules;

public class AssignScorePlayerRule : ActionConditionRule<IAssignScorePlayer>
{
    public AssignScorePlayerRule(IEnumerable<IAssignScorePlayer> rules, IEnumerable<ICondition> condition) : base(rules,
        condition, null)
    {
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
    {
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(game, ind))
            {
                this.Actions[i].AssignScore(game, rules, ind);
            }
        }
    }

    public AssignScorePlayerRule Clone()
    {
        return new AssignScorePlayerRule(this.Actions, this.Condition);
    }
}