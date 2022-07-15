using InfoGame;
using Table;
using Rules;

namespace Player;

public class Scorer<T>
{
    public delegate double MoveScorer(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves,GameStatus<T> game, InfoRules<T> rules, Random random, int Id);
    
    public static double RandomScorer(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves, GameStatus<T> game, InfoRules<T> rules, Random random, int Id)
    {
        return random.NextDouble();
    }

    public static double MostPopular(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves,GameStatus<T> game, InfoRules<T> rules, Random random, int Id)
    {
        //Console.WriteLine(move.Token + " " + strategiesMoves.Count(strategie => strategie.Contains(move)));
        return strategiesMoves.Count(strategie => strategie.Contains(move));
    }
}