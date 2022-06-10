using InfoGame;

namespace Rules;

//Falta determinar cuando los puntos de dos jugadores sean iguales
public interface IWinnerGame
{
    /// <summary>
    /// Determinar el ganador del juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public void Winner(GameStatus game, int ind);
}

public class WinnerGameMayor : IWinnerGame
{
    public void Winner(GameStatus game, int ind)
    {
        InfoPlayer[] aux = game.Players.ToArray();
        Array.Sort(aux, (o1, o2) => (o2.Score.CompareTo(o1.Score)));
        game.PlayerWinner = aux[0].ID;
        game.TeamWinner = game.FindTeamPlayer(aux[0].ID);
    }
}

public class WinnerGameMenor : IWinnerGame
{
    public void Winner(GameStatus game, int ind)
    {
        InfoPlayer[] aux = game.Players.ToArray();
        Array.Sort(aux, (o1, o2) => (o1.Score.CompareTo(o2.Score)));
        game.PlayerWinner = aux[0].ID;
        game.TeamWinner = game.FindTeamPlayer(aux[0].ID);
    }
}

public class WinnerGameTeamMayor : IWinnerGame
{
    public void Winner(GameStatus game, int ind)
    {
        double max = double.MinValue;
        int win = 0;
        for (int i = 0; i < game.Teams.Length; i++)
        {
            double sum = 0;
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                sum += game.Teams[i][j].Score;
            }

            if (sum > max)
            {
                max = sum;
                win = i;
            }
        }

        game.TeamWinner = win;
    }
}

public class WinnerGameTeamMenor : IWinnerGame
{
    public void Winner(GameStatus game, int ind)
    {
        double min = double.MaxValue;
        int win = 0;
        for (int i = 0; i < game.Teams.Length; i++)
        {
            double sum = 0;
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                sum += game.Teams[i][j].Score;
            }

            if (sum < min)
            {
                min = sum;
                win = i;
            }
        }

        game.TeamWinner = win;
    }
}