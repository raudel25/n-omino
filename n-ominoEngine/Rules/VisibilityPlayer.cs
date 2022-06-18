using Table;
using InfoGame;

namespace Rules;

public interface IVisibilityPlayer<T>
{
    /// <summary>
    /// Determinar la visibilidad de los jugadores sobre las fichas del juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public void Visibility(GameStatus<T> game, int ind);
}

public class ClassicVisibilityPlayer<T> : IVisibilityPlayer<T>
{
    public void Visibility(GameStatus<T> game, int ind)
    {
        for (int i = 0; i < game.Players.Length; i++)
        {
            if (i != game.Turns[ind])
            {
                game.Players[i].Hand = null;
            }
        }
    }
}

public class TeamVisibilityPlayer<T> : IVisibilityPlayer<T>
{
    public void Visibility(GameStatus<T> game, int ind)
    {
        List<List<Token<T>>> aux = new List<List<Token<T>>>();
        int team = game.FindTeamPlayer(game.Turns[ind]);

        //Guardamos las manos de los miembros del equipo
        for (int i = 0; i < game.Teams[team].Count; i++)
        {
            aux.Add(game.Teams[team][i].Hand!);
        }

        for (int i = 0; i < game.Players.Length; i++)
        {
            if (i != game.Turns[ind])
            {
                game.Players[i].Hand = null;
            }
        }

        //Reasignamos las manos de los miembros del equipo
        for (int i = 0; i < game.Teams[team].Count; i++)
        {
            game.Teams[team][i].Hand = aux[i];
        }
    }
}