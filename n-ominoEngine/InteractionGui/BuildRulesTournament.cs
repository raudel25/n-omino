using Rules;

namespace InteractionGui;

public class BuildRulesTournament<T>
{
    /// <summary>
    /// Determinar los jugadores que participan en cada juego
    /// </summary>
    public PlayerGameRule<T>? PlayerGame { get; set; }

    /// <summary>
    /// Asignar score a los jugadores en el torneo
    /// </summary>
    public ScorePlayerTournamentRule<T>? ScorePlayer { get; set; }

    /// <summary>
    /// Determinar la distribucion de los jugadores en el juego
    /// </summary>
    public DistributionPlayerRule<T>? DistributionPlayer { get; set; }

    /// <summary>
    /// Asignar score a los equipos en el torneo
    /// </summary>
    public ScoreTeamTournamentRule<T>? ScoreTeam { get; set; }

    /// <summary>
    /// Determinar los equipos que participan en cada juego
    /// </summary>
    public TeamsGameRule<T>? TeamGame { get; set; }

    /// <summary>
    /// Determinar el ganador del torneo
    /// </summary>
    public WinnerTournamentRule<T>? WinnerTournament { get; set; }

    public bool IsReady { get; set; }

    public InfoRulesTournament<T> Build()
    {
        return new InfoRulesTournament<T>(this.PlayerGame!, this.ScorePlayer!, this.DistributionPlayer!,
            this.ScoreTeam!, this.TeamGame!, this.WinnerTournament!);
    }

    public void Injection(PlayerGameRule<T> playerGame, ScorePlayerTournamentRule<T> scorePlayer,
        DistributionPlayerRule<T> distribution,
        ScoreTeamTournamentRule<T> scoreTeam, TeamsGameRule<T> teamGame,
        WinnerTournamentRule<T> winner)
    {
        this.DistributionPlayer = distribution;
        this.PlayerGame = playerGame;
        this.ScorePlayer = scorePlayer;
        this.ScoreTeam = scoreTeam;
        this.TeamGame = teamGame;
        this.WinnerTournament = winner;
        this.IsReady = true;
    }
}