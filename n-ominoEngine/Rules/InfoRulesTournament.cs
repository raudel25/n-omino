namespace Rules;

public class InfoRulesTournament<T>
{
    /// <summary>
    /// Determinar los jugadores que participan en cada juego
    /// </summary>
    public PlayerGameRule<T> PlayerGame { get; private set; }

    /// <summary>
    /// Asignar score a los jugadores en el torneo
    /// </summary>
    public ScorePlayerTournamentRule<T> ScorePlayer { get; private set; }

    /// <summary>
    /// Determinar la distribucion de los jugadores en el juego
    /// </summary>
    public DistributionPlayerRule<T> DistributionPlayer { get; private set; }

    /// <summary>
    /// Asignar score a los equipos en el torneo
    /// </summary>
    public ScoreTeamTournamentRule<T> ScoreTeam { get; private set; }

    /// <summary>
    /// Determinar los equipos que participan en cada juego
    /// </summary>
    public TeamsGameRule<T> TeamGame { get; private set; }

    /// <summary>
    /// Determinar el ganador del torneo
    /// </summary>
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