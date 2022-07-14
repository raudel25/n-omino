using InfoGame;

namespace Rules;

public interface IScorePlayerTournament<T>
{
    /// <summary>
    /// Asignar score a los jugadores en el torneo
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    /// <param name="game">Estado del actual juego</param>
    /// <param name="ind">Indice del torneo</param>
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, int ind);
}

public class ClassicScorePlayerTournament<T> : IScorePlayerTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, int ind)
    {
        int win = game.FindPLayerById(game.PlayerWinner);
        tournament.Players[win].ScoreTournament += 100;
        tournament.Players[win].GamesToWin++;
    }
}

public class GameScorePlayerTournament<T> : IScorePlayerTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, int ind)
    {
        int win = game.FindPLayerById(game.PlayerWinner);
        tournament.Players[win].ScoreTournament += game.Players[win].Score;
        tournament.Players[win].GamesToWin++;
    }
}