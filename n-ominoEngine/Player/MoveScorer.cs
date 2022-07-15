using InfoGame;
using Rules;

namespace Player;

public class Scorer<T>
{
    public delegate double MoveScorer(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves,GameStatus<T> game, InfoRules<T> rules, Random random, int id);
    
    public static double RandomScorer(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves, GameStatus<T> game, InfoRules<T> rules, Random random, int id)
    {
        return random.NextDouble();
    }

    public static double MostPopular(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves,GameStatus<T> game, InfoRules<T> rules, Random random, int id)
    {
        return strategiesMoves.Count(strategie => strategie.Contains(move));
    }
}