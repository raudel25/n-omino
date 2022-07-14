using InfoGame;

namespace Rules;

public interface ITeamsGame
{
    public void DeterminateTeams(TournamentStatus tournament, int ind);
}

public class ClassicTeam : ITeamsGame
{
    public void DeterminateTeams(TournamentStatus tournament, int ind)
    {
        for (int i = 0; i < tournament.ValidTeam.Length; i++)
        {
            tournament.ValidTeam[i] = true;
        }
    }
}