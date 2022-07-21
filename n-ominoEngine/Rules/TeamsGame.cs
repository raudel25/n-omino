using InfoGame;

namespace Rules;

public interface ITeamsGame
{
    public void DeterminateTeams(TournamentStatus tournament);
}

public class ClassicTeam : ITeamsGame
{
    public void DeterminateTeams(TournamentStatus tournament)
    {
        for (int i = 0; i < tournament.ValidTeam.Length; i++)
        {
            tournament.ValidTeam[i] = true;
        }
    }
}