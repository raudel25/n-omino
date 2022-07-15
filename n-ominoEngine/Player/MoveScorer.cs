using InfoGame;
using Table;
using Rules;

namespace Player;

public class Scorer<T>
{
    public delegate double MoveScorer(Move<T> move, GameStatus<T> game, InfoRules<T> rules, Random random);
    
    public static double RandomScorer(Move<T> move, GameStatus<T> game, InfoRules<T> rules, Random random)
    {
        return random.NextDouble();
    }
}