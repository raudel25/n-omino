using InfoGame;

namespace Rules;

public interface IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament);
}

public class ClassicPlayerGame : IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament)
    {
        for (int i = 0; i < tournament.ValidPlayer.Length; i++)
        {
            tournament.ValidPlayer[i] = true;
        }
    }
}

public class EvenPlayerGame : IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament)
    {
        for (int i = 0; i < tournament.ValidPlayer.Length; i++)
        {
            tournament.ValidPlayer[i] = (i & 1) == 0;
        }
    }
}

public class OddPlayerGame : IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament)
    {
        for (int i = 0; i < tournament.ValidPlayer.Length; i++)
        {
            tournament.ValidPlayer[i] = (i & 1) == 1;
        }
    }
}