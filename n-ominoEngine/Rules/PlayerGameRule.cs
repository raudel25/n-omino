using InfoGame;
using Table;

namespace Rules;

public class PlayerGameRule<T> : ActionConditionRule<IPlayerGame, T>, ICloneable<PlayerGameRule<T>>
{
    public PlayerGameRule(IEnumerable<IPlayerGame> rules, IEnumerable<ICondition<T>> condition,
        IPlayerGame rule) : base(rules, condition, rule)
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
                this.Actions[i].DeterminatePlayers(tournament);
                activate = true;
            }
        }

        if (!activate) this.Default!.DeterminatePlayers(tournament);
    }

    public PlayerGameRule<T> Clone()
    {
        return new PlayerGameRule<T>(this.Actions, this.Condition, this.Default!);
    }
}