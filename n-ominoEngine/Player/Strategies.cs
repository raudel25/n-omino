using Table;
using Rules;
using InfoGame;

namespace Player;

public interface IStrategy<T>
{
    //indice de la lista donde está la Move que quiero, si es -1 no hay Move posible
    public int Play(IList<Move<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id);
}

public class RandomPlayer<T> : IStrategy<T>
{
    public int Play(IList<Move<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        Random r = new Random();
        int index = r.Next(PossiblePlays.Count());
        return index;
    }
}

public class GreedyPlayer<T> : IStrategy<T>
{
    public int Play(IList<Move<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        //return PossiblePlays.MaxBy( x => rules.ScoreToken.ScoreToken(x.Token));
        int index = 0;
        Move<T> res = PossiblePlays[0];
        for (int i = 1; i < PossiblePlays.Count; i++)
            if (rules.ScoreToken.ScoreToken(res.Token!) < rules.ScoreToken.ScoreToken(PossiblePlays[i].Token!)) 
            {
                index = i;
                res = PossiblePlays[i];
            }
        return index;
    }
}


// public class PartnerPlayer<T> : IStrategy<T>
// {
//     IComparison<T>? comp;
//     public int Play(IList<Move<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules,int id)
//     {
//         //por cada jugador de mi equipo 
//         foreach(var player in status.Teams[status.FindTeamPlayer(id)]) 
//         {
//             //si soy yo, continúo
//             if(player.Id == id) continue;
//             //por cada valor que jugaron, si lo puedo poner, la pongo
//             foreach (var (value, cant) in player.History.Puestas)
//             {
//                 for (int i = 0; i < PossiblePlays.Count; i++)
//                 {
//                     if(PossiblePlays[i].Token!.Contains(value) && !PossibleMoves[i]Mata(value, PossiblePlays[i])) return i;
//                 }
//             }
//             rules.
//         }
//         return -1;
//     }
// }
// public class OponentPlayer<T> : IStrategy<T>
// {
//     public int Play(IList<Move<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id)
//     {
//         throw new NotImplementedException();
//     }
// }