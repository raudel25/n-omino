using Table;
using Rules;
using InfoGame;

namespace Player;

public interface IStrategy<T> where T : struct
{
    public Jugada<T> Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules);
}

public class RandomPlayer<T> : IStrategy<T> where T : struct
{
    public Jugada<T> Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules)
    {
        Random r = new Random();
        int index = r.Next(0, PossiblePlays.Count());
        return PossiblePlays[index];
    }
}

public class GreedyPlayer<T> : IStrategy<T> where T : struct
{
    public Jugada<T> Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules)
    {
        Jugada<T> res = PossiblePlays[0];
        for (int i = 0; i < PossiblePlays.Count; i++)
            if (rules.ScoreToken.ScoreToken(res.Token) < rules.ScoreToken.ScoreToken(PossiblePlays[i].Token)) res = PossiblePlays[i];
        return res;
    }
}

public class PartnerPlayer<T> : IStrategy<T> where T : struct
{
    public Jugada<T> Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules)
    {
        throw new NotImplementedException();
    }
}