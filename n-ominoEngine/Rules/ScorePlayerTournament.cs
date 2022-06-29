using InfoGame;

namespace Rules;

public interface IScorePlayerTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, int ind);
}

public class ClassicScorePlayerTournament<T> : IScorePlayerTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, int ind)
    {
        int win = game.PlayerWinner;
        tournament.Players[win].ScoreTournament = 100;
        tournament.Players[win].GamesToWin++;
    }
}