using InfoGame;

namespace Rules;

public interface IWinnerTournament
{
    public void DeterminateWinner(TournamentStatus tournament, int ind);
}

public class ClassicWinnerTournament : IWinnerTournament
{
    private int _socre;

    public ClassicWinnerTournament(int score)
    {
        this._socre = score;
    }
    public void DeterminateWinner(TournamentStatus tournament, int ind)
    {
        for (int i = 0; i < tournament.ScoreTeams.Length; i++)
        {
            if (tournament.ScoreTeams[i] >= this._socre)
            {
                tournament.TeamWinner = i;
                break;
            }
        }
    }
}