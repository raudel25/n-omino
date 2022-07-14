using InfoGame;

namespace Rules;

public interface IDistributionPlayer
{
    public void DeterminateDistribution(TournamentStatus tournament, int ind);
}

public class ClassicDistribution : IDistributionPlayer
{
    public void DeterminateDistribution(TournamentStatus tournament, int ind)
    {
        List<(int, int)> aux = new List<(int, int)>();

        bool[] visited = new bool[tournament.DistributionPlayers!.Count];

        int cant = 0;
        
        while (cant != visited.Length)
        {
            int index = -1;
            
            for (int j = 0; j < visited.Length; j++)
            {
                if(visited[j]) continue;
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