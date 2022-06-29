using InfoGame;

namespace Rules;

public interface IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament, int ind);
}

public class ClassicPlayerGame : IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament, int ind)
    {
        for (int i = 0; i < tournament.ValidPlayer.Length; i++)
        {
            tournament.ValidPlayer[i] = true;
        }
    }
}

public class EvenPlayerGame : IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament, int ind)
    {
        for (int i = 0; i < tournament.ValidPlayer.Length; i += 2)
        {
            tournament.ValidPlayer[i] = true;
        }
    }
}

public class OddPlayerGame : IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament, int ind)
    {
        for (int i = 1; i < tournament.ValidPlayer.Length; i += 2)
        {
            tournament.ValidPlayer[i] = true;
        }
    }
}