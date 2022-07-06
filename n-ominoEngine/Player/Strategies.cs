using Table;
using Rules;
using InfoGame;

namespace Player;

public interface IStrategy<T>
{
    //indice de la lista donde está la jugada que quiero, si es -1 no hay jugada posible
    public int Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id);
}

public class RandomPlayer<T> : IStrategy<T>
{
    public int Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        Random r = new Random();
        int index = r.Next(0, PossiblePlays.Count());
        return index;
    }
}

public class GreedyPlayer<T> : IStrategy<T>
{
    public int Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        //return PossiblePlays.MaxBy( x => rules.ScoreToken.ScoreToken(x.Token));
        int index = 0;
        Jugada<T> res = PossiblePlays[0];
        for (int i = 1; i < PossiblePlays.Count; i++)
            if (rules.ScoreToken.ScoreToken(res.Token!) < rules.ScoreToken.ScoreToken(PossiblePlays[i].Token!)) 
            {
                index = i;
                res = PossiblePlays[i];
            }
        return index;
    }
}

public class PartnerPlayer<T> : IStrategy<T>
{
    IComparison<T>? comp;
    public int Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules,int id)
    {
        //por cada jugador de mi equipo 
        foreach(var player in status.Teams[status.FindTeamPlayer(id)]) 
        {
            //si soy yo, continúo
            if(player.Id == id) continue;
            //por cada valor que jugaron, si lo puedo poner, la pongo
            foreach (var (value, cant) in player.History.Puestas)
            {
                for (int i = 0; i < PossiblePlays.Count; i++)
                {
                    if(PossiblePlays[i].Token!.Contains(value) && !Mata(value, PossiblePlays[i])) return i;
                }
            }
        }
        return -1;
    }

    public bool Mata (T value, Jugada<T> jugada)
    {
        //busco por los nodos si el valor T está en algún "no padre" retorno true
        for (int i = 0; i < jugada.Node!.Connections.Length; i++)
        {
            if(!jugada.Node.Fathers.Contains(jugada.Node.Connections[i]!)) continue;
            if(comp!.Compare(jugada.Node.Connections[i]!.ValueToken[i], value)) return true;
        }
        return false;
    }

}
public class OponentPlayer<T> : IStrategy<T>
{
    public int Play(IList<Jugada<T>> PossiblePlays, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        throw new NotImplementedException();
    }
}