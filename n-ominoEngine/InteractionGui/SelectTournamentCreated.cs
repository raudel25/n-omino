using Rules;

namespace InteractionGui;

public class SelectTournamentCreated<T>
{
    private PlayerGameRule<T> _playerGameRule => new PlayerGameRule<T>(Array.Empty<IPlayerGame>(),
        Array.Empty<ICondition<T>>(), new ClassicPlayerGame());

    private TeamsGameRule<T> _teamsGameRule =>
        new TeamsGameRule<T>(Array.Empty<ITeamsGame>(), Array.Empty<ICondition<T>>(), new ClassicTeam());

    private ScorePlayerTournamentRule<T> _scorePlayer => new ScorePlayerTournamentRule<T>(
        Array.Empty<IScorePlayerTournament<T>>(), Array.Empty<ICondition<T>>(), new ClassicScorePlayerTournament<T>());

    private ScoreTeamTournamentRule<T> _scoreTeam => new ScoreTeamTournamentRule<T>(
        Array.Empty<IScoreTeamTournament<T>>(), Array.Empty<ICondition<T>>(), new ClassicScoreTeamTournament<T>());

    private DistributionPlayerRule<T> _distribution => new DistributionPlayerRule<T>(Array.Empty<IDistributionPlayer>(),
        Array.Empty<ICondition<T>>(), new ClassicDistribution());

    private WinnerTournamentRule<T>[] _winner = new[]
    {
        new WinnerTournamentRule<T>(new[] {new ClassicWinnerTournament()}, new[] {new CantGamesTournament<T>(1)}),
        new WinnerTournamentRule<T>(new[] {new ClassicWinnerTournament()}, new[] {new CantGamesTournament<T>(3)}),
        new WinnerTournamentRule<T>(new[] {new ClassicWinnerTournament()}, new[] {new MaxScoreTeamTournament<T>(100)})
    };

    public InfoRulesTournament<T>[] Rules => new[]
    {
        new InfoRulesTournament<T>(_playerGameRule, _scorePlayer, _distribution, _scoreTeam, _teamsGameRule,
            _winner[0]),
        new InfoRulesTournament<T>(_playerGameRule, _scorePlayer, _distribution, _scoreTeam, _teamsGameRule,
            _winner[1]),
        new InfoRulesTournament<T>(_playerGameRule, _scorePlayer, _distribution, _scoreTeam, _teamsGameRule,
            _winner[2])
    };

    public (string, int)[] NameTournaments => new[]
    {
        ("Un solo juego", 0),
        ("Torneo clásico (de 3 a ganar 2)", 1),
        ("Torneo clásico (100 puntos)", 2)
    };
}