using Table;
using InfoGame;

namespace Rules;

public interface IVisibilityPlayer
{
    /// <summary>
    /// Determinar la visibilidad de los jugadores sobre las fichas del juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="player">ID del jugador que le corresponde jugar</param>
    public void Visibility(GameStatus game, int player);
}

public class ClasicVisibilityPlayer : IVisibilityPlayer
{
    public void Visibility(GameStatus game, int player)
    {
        for (int i = 0; i < game.Players.Length; i++)
        {
            if (i != player)
            {
                game.Players[i].Hand = null;
            }
        }
    }
}

public class TeamVisibilityPlayer : IVisibilityPlayer
{
    public void Visibility(GameStatus game, int player)
    {
        List<HashSet<Token>> aux = new List<HashSet<Token>>();
        //Guardamos las manos de los miembros del equipo
        for (int i = 0; i < game.Teams[player].Count; i++)
        {
            aux.Add(game.Teams[player][i].Hand!);
        }

        for (int i = 0; i < game.Players.Length; i++)
        {
            if (i != player)
            {
                game.Players[i].Hand = null;
            }
        }

        //Reasignamos las manos de los miembros del equipo
        for (int i = 0; i < game.Teams[player].Count; i++)
        {
            game.Teams[player][i].Hand = aux[i];
        }
    }
}