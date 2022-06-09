using InfoGame;

namespace Rules;

public interface ICondition
{
    public bool RunRule(GameStatus game, int ind);
}