using InfoGame;

namespace Rules;

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

        var team = game.FindTeamById(tournament.ImmediateWinnerTeam);

        //Comprobamos si el equipo ganador esta en el juego
        if (team == -1) return;

        foreach (var player in game.Teams[team])
        {
            var rnd = new Random();
            var remove = rnd.Next(player.HandCount);

            var i = 0;

            foreach (var token in player.Hand)
            {
                var aux = token;
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