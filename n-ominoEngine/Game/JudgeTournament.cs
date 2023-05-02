using InfoGame;
using Player;
using Rules;

namespace Game;

public class JudgeTournament<T>
{
    /// <summary>
    ///     Contructores de juego
    /// </summary>
    private readonly List<BuildGame<T>> _games;

    /// <summary>
    ///     Jugadores
    /// </summary>
    private readonly List<Player<T>> _playersPlay;

    /// <summary>
    ///     Estado del torneo
    /// </summary>
    private readonly TournamentStatus _tournament;

    /// <summary>
    ///     Reglas del torneo
    /// </summary>
    private readonly InfoRulesTournament<T> _tournamentRules;

    public JudgeTournament(List<BuildGame<T>> games, List<Player<T>> playersPlay,
        List<(int, int, string)> playerTeams, InfoRulesTournament<T> rulesTournament)
    {
        SortTeam(playerTeams);
        _games = games;
        _playersPlay = playersPlay;
        _tournament = CreateTournament(playerTeams);
        _tournamentRules = rulesTournament;
    }

    /// <summary>
    ///     Crear un torneo
    /// </summary>
    /// <param name="playerTeams">Distribucion de jugadores</param>
    /// <returns>Estado del torneo</returns>
    private TournamentStatus CreateTournament(List<(int, int, string)> playerTeams)
    {
        var players = new List<InfoPlayerTournament>();
        var teams = new List<InfoTeams<InfoPlayerTournament>>();

        //Determinar la distribucion de los jugadores
        var teamId = -1;
        foreach (var item in playerTeams)
        {
            if (item.Item1 != teamId)
            {
                teams.Add(new InfoTeams<InfoPlayerTournament>(item.Item1));
                teamId = item.Item1;
            }

            var aux = new InfoPlayerTournament(item.Item3, item.Item2);
            teams[teams.Count - 1].Add(aux);
            players.Add(aux);
        }

        return new TournamentStatus(players, teams);
    }

    public void TournamentGame()
    {
        var ind = 0;
        var init = _games[0].Initializer.StartGame(new List<(int, int, string)> { (0, 0, "") });

        while (!EndTournament())
            for (var i = 0; i < _games.Count; i++)
            {
                _tournament.Index = ind++;

                PreGame(init, i);

                var playerTeams = _tournament.DistributionPlayers!;

                //Crear los players para usar en el juego
                var players = new List<Player<T>>();

                for (var j = 0; j < playerTeams.Count; j++) players.Add(_playersPlay[playerTeams[j].Item2]);

                if (players.Count == 0) continue;

                _games[i] = _games[i].Reset();

                init = _games[i].Initializer.StartGame(playerTeams);

                //Desarrollar un juego
                var judge = new Judge<T>(_tournament, _games[i].Rules, init, players,
                    _games[i].Print);
                judge.Game();

                PostGame(i, init);

                Printer.ExecuteMessageEvent("El equipo " + _tournament.ImmediateWinnerTeam +
                                            " ha ganado el actual juego");

                if (EndTournament()) break;

                _games[i] = _games[i].Reset();
            }

        Printer.ExecuteMessageEvent("El equipo " + _tournament.TeamWinner + " ha ganado torneo");
        Printer.ExecuteResetEvent();
    }

    /// <summary>
    ///     Analizar las reglas a ejecutar antes del juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    private void PreGame(GameStatus<T> game, int typeTournament)
    {
        _tournamentRules.PlayerGame.RunRule(_tournament, game, _games[typeTournament].Rules.ScoreToken,
            0);
        _tournamentRules.TeamGame.RunRule(_tournament, game, _games[typeTournament].Rules.ScoreToken,
            0);
        _tournament.DistributionPlayers = DeterminatePlayerTeams();
        _tournamentRules.DistributionPlayer.RunRule(_tournament, game,
            _games[typeTournament].Rules.ScoreToken, 0);
    }

    /// <summary>
    ///     Analizar las reglas a ejecutar despues del juego
    /// </summary>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    /// <param name="game">Estado del juego</param>
    private void PostGame(int typeTournament, GameStatus<T> game)
    {
        _tournament.ImmediateWinner = game.PlayerWinner;
        _tournament.ImmediateWinnerTeam = game.TeamWinner;
        _tournamentRules.ScorePlayer.RunRule(_tournament, game, game,
            _games[typeTournament].Rules.ScoreToken, game.LastIndex);
        _tournamentRules.ScoreTeam.RunRule(_tournament, game, _games[typeTournament].Rules.ScoreToken,
            game.LastIndex);
        _tournamentRules.WinnerTournament.RunRule(_tournament, game,
            _games[typeTournament].Rules.ScoreToken,
            game.LastIndex);
    }

    /// <summary>
    ///     Determinar cuando finaliza un torneo
    /// </summary>
    /// <returns>Si el torneo termino</returns>
    private bool EndTournament()
    {
        return _tournament.TeamWinner != -1;
    }

    /// <summary>
    ///     Determinar la distribucion de los jugadores
    /// </summary>
    /// <returns>Distribucion de los jugadores</returns>
    private List<(int, int, string)> DeterminatePlayerTeams()
    {
        var aux = new List<(int, int, string)>();

        for (var i = 0; i < _tournament.Teams.Count; i++)
            if (_tournament.ValidTeam[i])
                foreach (var item in _tournament.Teams[i])
                    if (_tournament.ValidPlayer[item.Id])
                        aux.Add((_tournament.Teams[i].Id, item.Id, item.Name));

        return aux;
    }

    private void SortTeam(List<(int, int, string)> players)
    {
        players.Sort();

        var value = -1;
        var index = -1;
        for (var i = 0; i < players.Count; i++)
        {
            if (value != players[i].Item1)
            {
                index++;
                value = players[i].Item1;
            }

            players[i] = (index, i, players[i].Item3);
        }
    }
}