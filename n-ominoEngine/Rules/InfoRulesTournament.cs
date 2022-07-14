namespace Rules;

public class InfoRulesTournament<T>
{
    public PlayerGameRule<T> PlayerGame { get; private set; }
    public ScorePlayerTournamentRule<T> ScorePlayer { get; private set; }
    public DistributionPlayerRule<T> DistributionPlayer { get; private set; }
    public ScoreTeamTournamentRule<T> ScoreTeam { get; private set; }
    public TeamsGameRule<T> TeamGame { get; private set; }
    public WinnerTournamentRule<T> WinnerTournament { get; private set; }

    public InfoRulesTournament(PlayerGameRule<T> playerGame, ScorePlayerTournamentRule<T> scorePlayer,
        DistributionPlayerRule<T> distribution,
        ScoreTeamTournamentRule<T> scoreTeam, TeamsGameRule<T> teamGame,
        WinnerTournamentRule<T> winner)
    {
        this.PlayerGame = playerGame;
        this.ScorePlayer = scorePlayer;
        this.ScoreTeam = scoreTeam;
        this.TeamGame = teamGame;
        this.WinnerTournament = winner;
        this.DistributionPlayer = distribution;
    }
}