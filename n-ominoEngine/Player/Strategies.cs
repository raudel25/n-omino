using Table;
using Rules;
using InfoGame;

namespace Player;

public interface IStrategy<T>
{
    public Move<T> Play(IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id);
}

public class RandomPlayer<T> : IStrategy<T>
{
    Random r = new Random();
    public Move<T> Play(IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        int index = r.Next(PossibleMoves.Count());
        return PossibleMoves.ElementAt(index);
    }
}

public class GreedyPlayer<T> : IStrategy<T>
{
    //Func<Token<T>, Token<T>, bool> comp;
    public Move<T> Play(IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        // Move<T> res = PossibleMoves.ElementAt(0);
        // for (int i = 1; i < PossibleMoves.Count(); i++)
        //     if (rules.ScoreToken.ScoreToken(res.Token!) < rules.ScoreToken.ScoreToken(PossibleMoves.ElementAt(i).Token!)) 
        //     {
        //         res = PossibleMoves.ElementAt(i);
        //     }
        return PossibleMoves.MaxBy(x => rules.ScoreToken.ScoreToken(x.Token!))!;
    }
}

public class TeamPlayer<T> : IStrategy<T>
{
    public Move<T> Play(IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var FavMoves = GetFavTeamMoves(PossibleMoves, status, rules,id);
        if(FavMoves.Count() == 0) return new Move<T>(null!, null!, -1);
        return FavMoves.First();
    }
    public IEnumerable<Move<T>> GetFavTeamMoves (IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        foreach (var (value, cant) in GetTeamPuestas(status, status.FindTeamPlayer(id)).OrderByDescending(x => x.cant))
        {
            foreach (var item in PossibleMoves.Where( x => (x.Token!.Contains(value) && !x.Mata(value)) && x.Node!.Fathers.Any(y => (status.FindTeamPlayer(y.IdPlayer) == status.FindPLayerById(id)))))
            {
                yield return item;
            }
        }
    }

    public IEnumerable<(T value, int cant)> GetTeamPuestas(GameStatus<T> status, int team)
    {
        foreach(var value in status.Values)
        {
            int sum = 0;
            foreach (var player in status.Teams[team])
            {
                sum += player.History.HowManyPuso(value);
            }
            yield return (value,sum)!;
        }
    }
}

public class EnemyPlayer<T> : IStrategy<T>
{
    //Juega la ficha que más han matado los oponentes
    public Move<T> Play(IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var FavMoves = GetKillEnemyMoves(PossibleMoves, status, rules,id);
        if(FavMoves.Count() == 0) return new Move<T>(null!, null!, -1);
        return FavMoves.First();
    }
    public IEnumerable<Move<T>> GetKillEnemyMoves (IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        foreach (var (value, cant) in GetEnemyTeamsMatadas(status, status.FindTeamPlayer(id)).OrderByDescending(x => x.cant))
        {
            foreach (var item in PossibleMoves.Where( x => (x.Token!.Contains(value) && !x.Mata(value))))
            {
                yield return item;
            }
        }
    }

    public IEnumerable<(T value, int cant)> GetEnemyTeamsMatadas(GameStatus<T> status, int team)
    {
        foreach(var value in status.Values)
        {
            int sum = 0;
            foreach (var player in Enemies(status, team))
            {
                sum += player.History.HowManyMato(value);
            }
            yield return (value,sum)!;
        }
    }
    public IEnumerable<InfoPlayer<T>> Enemies(GameStatus<T> status, int team)
    {
        for (int i = 0; i < status.Teams.Count; i++)
        {
            if(i == team) continue;
            foreach (var player in status.Teams[i])
            {
                yield return player;
            }
        }
    }
}

public class SinglePlayer<T> : IStrategy<T>
{ 
    //Juega para él, intenta no quedarse al fallo, jugando siempre la ficha que más tiene y 
    //de las cumplan la condicion anterior bota la mas gorda
    public Move<T> Play(IEnumerable<Move<T>> PossibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var myHand = status.Players[status.FindPLayerById(id)].Hand;
        var a = InHand(myHand, status).OrderByDescending(x => x.cant);
        PossibleMoves = PossibleMoves.Where(x => x.Token!.Contains(a.First().value));
        Move<T> move = PossibleMoves.MaxBy( x => rules.ScoreToken.ScoreToken(x.Token!))!;
        foreach(var val in a.Where(x => x.cant == 1))
        {
            PossibleMoves = PossibleMoves.Where(x => !x.Token!.Contains(val.value));
        }
        if(PossibleMoves.Count() != 0) move = PossibleMoves.MaxBy( x => rules.ScoreToken.ScoreToken(x.Token!))!;
        return move;
    }

    public IEnumerable<(T value, int cant)> InHand(Hand<T> hand, GameStatus<T> status)
    {
        foreach (var value in status.Values)
        {
            int cant = 0;
            foreach (var token in hand)
            {
                if(token.Contains(value)) cant++;
            }
            yield return (value, cant);
        }
    }
}