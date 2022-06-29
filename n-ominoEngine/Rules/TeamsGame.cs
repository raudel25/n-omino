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
        
    }
}