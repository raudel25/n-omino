using InfoGame;

namespace Rules;

public interface IDistributionPlayer
{
    /// <summary>
    /// Determinar la distribucion de los players en el juego
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    public void DeterminateDistribution(TournamentStatus tournament);
}

public class ClassicDistribution : IDistributionPlayer
{
    public void DeterminateDistribution(TournamentStatus tournament)
    {
        List<(int, int, string)> aux = new List<(int, int, string)>();

        bool[] visited = new bool[tournament.DistributionPlayers!.Count];

        int cant = 0;

        while (cant != visited.Length)
        {
            int index = -1;

            for (int j = 0; j < visited.Length; j++)
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