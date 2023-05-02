using InfoGame;
using Table;

namespace Rules;

public class PlayerGameRule<T> : ActionConditionRule<IPlayerGame, T>, ICloneable<PlayerGameRule<T>>
{
    public PlayerGameRule(IEnumerable<IPlayerGame> rules, IEnumerable<ICondition<T>> condition,
        IPlayerGame rule) : base(rules, condition, rule)
    {
    }

    public PlayerGameRule<T> Clone()
    {
        return new PlayerGameRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        var activate = false;
        for (var i = 0; i < Condition.Length; i++)
            if (Condition[i].RunRule(tournament, original, rules, ind))
            {
                Actions[i].DeterminatePlayers(tournament);
                activate = true;
            }

        if (!activate) Default!.DeterminatePlayers(tournament);
    }
}