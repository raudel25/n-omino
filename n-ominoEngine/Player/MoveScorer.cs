using InfoGame;
using Rules;

namespace Player;

public class Scorer<T>
{
    //otorga un valor numérico a move
    public delegate double MoveScorer(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves,
        GameStatus<T> game, InfoRules<T> rules, Random random, int id);

    //otorga un valor random a la jugada
    public static double RandomScorer(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves,
        GameStatus<T> game, InfoRules<T> rules, Random random, int id)
    {
        return random.NextDouble();
    }

    //otorga a la jugada la cantidad de estrategias en que aparece
    public static double MostPopular(Move<T> move, IEnumerable<IEnumerable<Move<T>>> strategiesMoves,
        GameStatus<T> game, InfoRules<T> rules, Random random, int id)
    {
        return strategiesMoves.Count(strategie => strategie.Contains(move));
    }
}