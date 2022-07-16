using Table;
using Rules;
using InfoGame;

namespace Player;

public interface IStrategy<T>
{
    public IEnumerable<Move<T>> Play(IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id);
}

//Ordena las jugadas aleatoriamente
public class RandomPlayer<T> : IStrategy<T>
{
    Random r = new Random();

    IEnumerable<Move<T>> IStrategy<T>.Play(IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var moves = possibleMoves.OrderBy(x => r.NextDouble());
        return moves;
    }
}

//Ordena las jugadas de mayor a menor score de la ficha según la regla
public class GreedyPlayer<T> : IStrategy<T>
{
    IEnumerable<Move<T>> IStrategy<T>.Play(IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var moves = possibleMoves.OrderByDescending(x => rules.ScoreToken.ScoreToken(x.Token!));
        return moves;
    }
}

//Ordeno las jugadas que favorecen a mi equipo
public class TeamPlayer<T> : IStrategy<T>
{
    public IEnumerable<Move<T>> GetFavTeamMoves (IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        //busco los valores que más ha puesto mi equipo
        foreach (var (value, cant) in GetTeamPuestas(status, status.FindTeamPlayer(id)).OrderByDescending(x => x.cant))
        {
            //reviso que con la jugada no mate ese valor o mate alguna ficha de alguien de mi equipo
            foreach (var item in possibleMoves.Where( x => (x.Token!.Contains(value) && !Mata(x.Node!, value)) && !MataTeam(x.Node!,id,status)))
            {
                yield return item;
            }
        }
    }

    //determina si jugando por ese nodo matas una de tu equipo
    public bool MataTeam(INode<T> node, int id, GameStatus<T> status)
    {
        foreach (var conection in node.Connections)
        {
            if(conection! is null) continue;
            if(status.FindTeamPlayer(conection!.IdPlayer) == id) return false;
            if(status.FindTeamPlayer(conection!.IdPlayer) == status.FindTeamPlayer(id)) return true;
        }
        return false;
    }
    
    //determina si jugando por ese nodo matas el valor
    public bool Mata(INode<T> node, T value)
    {
        if(node.ValueToken is not null) return false;
        for (int i = 0; i < node.Connections.Length; i++)
        {
            if(node.Connections[i] is null) continue;
            if(object.Equals(node.Connections[i]!.ValueToken[i], value)) return true;
        }
        return false;
    }

    //devuelve para cada valor la cantidad de veces que lo ha puesto alguien del equipo
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

    IEnumerable<Move<T>> IStrategy<T>.Play(IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var moves = GetFavTeamMoves(possibleMoves, status, rules,id);
        return moves;
    }
}

public class EnemyPlayer<T> : IStrategy<T>
{
    //Devuelve las jugadas en la que pongo la ficha que mató un oponente
    public IEnumerable<Move<T>> GetKillEnemyMoves (IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        foreach (var (value, cant) in GetEnemyTeamsMatadas(status, status.FindTeamPlayer(id)).OrderByDescending(x => x.cant))
        {
            foreach (var item in possibleMoves.Where( x => (x.Token!.Contains(value) && !x.Mata(value))))
            {
                yield return item;
            }
        }
    }

    //Determina cuántas fichas jugaron los equipos contrarios para matar cada valor
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

    //Encuentra quienes son los oponentes
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

    IEnumerable<Move<T>> IStrategy<T>.Play(IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var moves = GetKillEnemyMoves(possibleMoves, status, rules,id);
        return moves;
    }
}

//Devuelve un IEnumerable con las jugadas que contienen la ficha de la que más cantidad tengo
public class SinglePlayer<T> : IStrategy<T>
{ 
    public IEnumerable<Move<T>> GetMovesWithValue(T value, IEnumerable<Move<T>> possiblemoves) => possiblemoves.Where(move => move.Token!.Contains(value));

    //Cuántas fichas de la mano contienen cada valor
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

    IEnumerable<Move<T>> IStrategy<T>.Play(IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var myHand = status.Players[status.FindPLayerById(id)].Hand;
        var values = InHand(myHand, status).OrderByDescending(x => x.cant);
        //busco por los valores que más tengo en la mano si se pueden jugar
        //cuando encuentre uno lo devuelvo
        IEnumerable<Move<T>> moves = GetMovesWithValue(values.First().value, possibleMoves);
        foreach (var value in values.Skip(1))
        {
            if(moves.Count() != 0) break;
            moves = GetMovesWithValue(value.value, possibleMoves);
        }
        return moves;
    }   
}

//Devuelve un IEnumerable de jugadas con aquellas con las que no se queda al fallo
public class NoQuedarseAlFallo<T> : IStrategy<T>
{
    public IEnumerable<Move<T>> Play(IEnumerable<Move<T>> possibleMoves, GameStatus<T> status, InfoRules<T> rules, int id)
    {
        var myHand = status.Players[status.FindPLayerById(id)].Hand;
        //reviso que las jugadas que voy a devolver no contengan un valor del cual me quede una sola ficha
        var values = InHand(myHand, status).Where(x => x.cant == 1);
        var moves = possibleMoves.Where(x => !values.Any(value => x.Token!.Contains(value.value)));
        return moves;
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