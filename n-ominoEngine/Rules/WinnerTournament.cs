using InfoGame;

namespace Rules;

public interface IWinnerTournament
{
    /// <summary>
    /// Determinar el ganador de un torneo
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    /// <param name="ind">Indice del torneo</param>
    public void DeterminateWinner(TournamentStatus tournament, int ind);
}

public class ClassicWinnerTournament : IWinnerTournament
{
    public void DeterminateWinner(TournamentStatus tournament, int ind)
    {
        double max = int.MinValue;
        int index = -1;

        for (int i = 0; i < tournament.ScoreTeams.Length; i++)
        {
            if (tournament.ScoreTeams[i] > max)
            {
                max = tournament.ScoreTeams[i];
                index = i;
            }
        }

        tournament.TeamWinner = index;
    }
}

public class MaxPlayerScore : IWinnerTournament
{
    public void DeterminateWinner(TournamentStatus tournament, int ind)
    {
        double max = double.MinValue;
        int index = -1;

        for (int i = 0; i < tournament.Teams.Count; i++)
        {
            double sum = 0;

            foreach (var item in tournament.Teams[i])
            {
                sum += item.ScoreTournament;
            }

            if (sum > max)
            {
                max = sum;
                index = i;
            }
        }

        tournament.TeamWinner = index;
    }
}