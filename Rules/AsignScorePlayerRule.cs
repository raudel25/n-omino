using InfoGame;

namespace Rules;

public class AsignScorePlayerRule : ActionConditionRule<IAsignScorePlayer>
{
    public AsignScorePlayerRule(IEnumerable<IAsignScorePlayer> rules, IEnumerable<ICondition> condition) : base(rules,
        condition, null)
    {
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
    {
        for (int i = 0; i < this.Critery.Length; i++)
        {
            if (this.Critery[i].RunRule(game, ind))
            {
                this.Actions[i].AsignScore(game, rules, ind);
            }
        }
    }

    public AsignScorePlayerRule Clone()
    {
        return new AsignScorePlayerRule(this.Actions, this.Critery);
    }
}