namespace Rules;

public class InfoRulesTournament<T>
{
    public InfoRulesTournament(PlayerGameRule<T> playerGame, ScorePlayerTournamentRule<T> scorePlayer,
        DistributionPlayerRule<T> distribution,
        ScoreTeamTournamentRule<T> scoreTeam, TeamsGameRule<T> teamGame,
        WinnerTournamentRule<T> winner)
    {
        PlayerGame = playerGame;
        ScorePlayer = scorePlayer;
        ScoreTeam = scoreTeam;
        TeamGame = teamGame;
        WinnerTournament = winner;
        DistributionPlayer = distribution;
    }

    /// <summary>
    ///     Determinar los jugadores que participan en cada juego
    /// </summary>
    public PlayerGameRule<T> PlayerGame { get; }

    /// <summary>
    ///     Asignar score a los jugadores en el torneo
    /// </summary>
    public ScorePlayerTournamentRule<T> ScorePlayer { get; }

    /// <summary>
    ///     Determinar la distribucion de los jugadores en el juego
    /// </summary>
    public DistributionPlayerRule<T> DistributionPlayer { get; }

    /// <summary>
    ///     Asignar score a los equipos en el torneo
    /// </summary>
    public ScoreTeamTournamentRule<T> ScoreTeam { get; }

    /// <summary>
    ///     Determinar los equipos que participan en cada juego
    /// </summary>
    public TeamsGameRule<T> TeamGame { get; }

    /// <summary>
    ///     Determinar el ganador del torneo
    /// </summary>
    public WinnerTournamentRule<T> WinnerTournament { get; }
}