using InfoGame;

namespace Rules;

public class ClassicDistribution : IDistributionPlayer
{
    public void DeterminateDistribution(TournamentStatus tournament)
    {
        var aux = new List<(int, int, string)>();

        var visited = new bool[tournament.DistributionPlayers!.Count];

        var cant = 0;

        while (cant != visited.Length)
        {
            var index = -1;

            for (var j = 0; j < visited.Length; j++)
            {
                if (visited[j]) continue;
                if (tournament.DistributionPlayers![j].Item1 != index)
                {
                    visited[j] = true;
                    aux.Add(tournament.DistributionPlayers![j]);
                    index = tournament.DistributionPlayers![j].Item1;
                    cant++;
                }
            }
        }

        tournament.DistributionPlayers = aux;
    }
}

public class TeamDistribution : IDistributionPlayer
{
    public void DeterminateDistribution(TournamentStatus tournament)
    {
    }
}