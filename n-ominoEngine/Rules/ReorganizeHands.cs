using InfoGame;
using Table;

namespace Rules;

public interface IReorganizeHands<T>
{
    public void Reorganize(TournamentStatus tournament, GameStatus<T> game);
}

public class ClassicReorganize<T> : IReorganizeHands<T>
{
    public void Reorganize(TournamentStatus tournament, GameStatus<T> game)
    {
    }
}

public class HandsTeamWin<T> : IReorganizeHands<T>
{
    public void Reorganize(TournamentStatus tournament, GameStatus<T> game)
    {
        if (tournament.ImmediateWinnerTeam == -1) return;

        int team = game.FindTeamById(tournament.ImmediateWinnerTeam);

        foreach (var player in game.Teams[team])
        {
            Random rnd = new Random();
            int remove = rnd.Next(player.HandCount);

            int i = 0;

            foreach (var token in player.Hand)
            {
                Token<T> aux = token;
                if (i == remove)
                {
                    player.Hand.Remove(aux);
                    break;
                }

                i++;
            }
        }
    }
}