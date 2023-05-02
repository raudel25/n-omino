using InfoGame;

namespace Rules;

public class ClassicScorePlayerTournament<T> : IScorePlayerTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game)
    {
        var win = game.FindPLayerById(game.PlayerWinner);
        tournament.Players[win].ScoreTournament += 100;
        tournament.Players[win].GamesToWin++;
    }
}

public class GameScorePlayerTournament<T> : IScorePlayerTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game)
    {
        var win = game.FindPLayerById(game.PlayerWinner);
        tournament.Players[win].ScoreTournament += game.Players[win].Score;
        tournament.Players[win].GamesToWin++;
    }
}