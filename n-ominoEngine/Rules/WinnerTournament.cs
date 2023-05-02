using InfoGame;

namespace Rules;

public class ClassicWinnerTournament : IWinnerTournament
{
    public void DeterminateWinner(TournamentStatus tournament)
    {
        double max = int.MinValue;
        var index = -1;

        for (var i = 0; i < tournament.ScoreTeams.Length; i++)
            if (tournament.ScoreTeams[i] > max)
            {
                max = tournament.ScoreTeams[i];
                index = i;
            }

        tournament.TeamWinner = index;
    }
}

public class MaxPlayerScore : IWinnerTournament
{
    public void DeterminateWinner(TournamentStatus tournament)
    {
        var max = double.MinValue;
        var index = -1;

        for (var i = 0; i < tournament.Teams.Count; i++)
        {
            double sum = 0;

            foreach (var item in tournament.Teams[i]) sum += item.ScoreTournament;

            if (sum > max)
            {
                max = sum;
                index = i;
            }
        }

        tournament.TeamWinner = index;
    }
}