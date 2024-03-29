using InfoGame;

namespace Rules;

public class ClassicTeam : ITeamsGame
{
    public void DeterminateTeams(TournamentStatus tournament)
    {
        for (var i = 0; i < tournament.ValidTeam.Length; i++) tournament.ValidTeam[i] = true;
    }
}

public class LeagueTeam : ITeamsGame
{
    private readonly int _cantTeams;
    private bool _init;
    private readonly List<List<int>> _plays;

    public LeagueTeam(int cant)
    {
        _cantTeams = cant;
        _plays = new List<List<int>>();
    }

    public void DeterminateTeams(TournamentStatus tournament)
    {
        if (!_init)
        {
            Combinations(tournament.ValidTeam.Length, _plays, new bool[tournament.ValidTeam.Length], 0);
            if (_plays.Count == 0) _plays.Add(new List<int>());
            _init = true;
        }

        var ind = tournament.Index % _plays.Count;

        for (var i = 0; i < tournament.ValidTeam.Length; i++) tournament.ValidTeam[i] = false;

        for (var i = 0; i < _plays[ind].Count; i++) tournament.ValidTeam[_plays[ind][i]] = true;
    }

    private void Combinations(int n, List<List<int>> plays, bool[] visited, int index)
    {
        if (_cantTeams == index)
        {
            plays.Add(new List<int>());
            for (var i = 0; i < n; i++)
                if (visited[i])
                    plays[plays.Count - 1].Add(i);
        }

        for (var i = index; i < n; i++)
        {
            if (visited[i]) continue;

            visited[i] = true;
            Combinations(n, plays, visited, index + 1);
            visited[i] = false;
        }
    }
}