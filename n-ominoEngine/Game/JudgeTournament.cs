using InfoGame;
using Player;
using Rules;

namespace Game;

public class JudgeTournament<T>
{
    /// <summary>
    /// Contructores de juego
    /// </summary>
    private List<BuildGame<T>> _games;

    /// <summary>
    /// Jugadores
    /// </summary>
    private List<Player<T>> _playersPlay;

    /// <summary>
    /// Estado del torneo
    /// </summary>
    private TournamentStatus _tournament;

    /// <summary>
    /// Reglas del torneo
    /// </summary>
    private InfoRulesTournament<T> _tournamentRules;

    public JudgeTournament(List<BuildGame<T>> games, List<Player<T>> playersPlay,
        List<(int, int,string)> playerTeams, InfoRulesTournament<T> rulesTournament)
    {
        this.SortTeam(playerTeams);
        this._games = games;
        this._playersPlay = playersPlay;
        this._tournament = CreateTournament(playerTeams);
        this._tournamentRules = rulesTournament;
    }

    /// <summary>
    /// Crear un torneo
    /// </summary>
    /// <param name="playerTeams">Distribucion de jugadores</param>
    /// <returns>Estado del torneo</returns>
    private TournamentStatus CreateTournament(List<(int, int, string)> playerTeams)
    {
        List<InfoPlayerTournament> players = new List<InfoPlayerTournament>();
        List<InfoTeams<InfoPlayerTournament>> teams = new List<InfoTeams<InfoPlayerTournament>>();

        //Determinar la distribucion de los jugadores
        int teamId = -1;
        foreach (var item in playerTeams)
        {
            if (item.Item1 != teamId)
            {
                teams.Add(new InfoTeams<InfoPlayerTournament>(item.Item1));
                teamId = item.Item1;
            }

            var aux = new InfoPlayerTournament(item.Item3,item.Item2);
            teams[teams.Count - 1].Add(aux);
            players.Add(aux);
        }

        return new TournamentStatus(players, teams);
    }

    public void TournamentGame()
    {
        int ind = 0;
        while (!EndTournament())
        {
            for (int i = 0; i < this._games.Count; i++)
            {
                GameStatus<T> aux = _games[i].Initializer.StartGame(new List<(int, int,string)>());

                PreGame(ind, aux, i);

                List<(int, int,string)> playerTeams = this._tournament.DistributionPlayers!;

                //Crear los players para usar en el juego
                List<Player<T>> players = new List<Player<T>>();

                for (int j = 0; j < playerTeams.Count; j++)
                {
                    players.Add(this._playersPlay[playerTeams[j].Item2]);
                }

                _games[i] = _games[i].Reset();

                GameStatus<T> init = this._games[i].Initializer.StartGame(playerTeams);

                //Desarrollar un juego
                Judge<T> judge = new Judge<T>(this._tournament, this._games[i].Rules, init, players,
                    this._games[i].Print);
                judge.Game();

                PostGame(ind, i, init);

                Printer.ExecuteMessageEvent("El equipo " + this._tournament.ImmediateWinnerTeam +
                                            " ha ganado el actual juego");

                if (EndTournament()) break;

                ind++;

                _games[i] = _games[i].Reset();
            }
        }

        Printer.ExecuteMessageEvent("El equipo " + this._tournament.TeamWinner + " ha ganado torneo");
    }

    /// <summary>
    /// Analizar las reglas a ejecutar antes del juego
    /// </summary>
    /// <param name="ind">Indice del torneo</param>
    /// <param name="game">Estado del juego</param>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    private void PreGame(int ind, GameStatus<T> game, int typeTournament)
    {
        this._tournamentRules.PlayerGame.RunRule(this._tournament, game, game, this._games[typeTournament].Rules,
            ind);
        this._tournamentRules.TeamGame.RunRule(this._tournament, game, game, this._games[typeTournament].Rules, ind);
        _tournament.DistributionPlayers = DeterminatePlayerTeams();
        this._tournamentRules.DistributionPlayer.RunRule(this._tournament, game, game,
            this._games[typeTournament].Rules, ind);
        this._tournament.Index = ind;
    }

    /// <summary>
    /// Analizar las reglas a ejecutar despues del juego
    /// </summary>
    /// <param name="ind">Indice del torneo</param>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    /// <param name="game">Estado del juego</param>
    private void PostGame(int ind, int typeTournament, GameStatus<T> game)
    {
        this._tournament.ImmediateWinner = game.PlayerWinner;
        this._tournament.ImmediateWinnerTeam = game.TeamWinner;
        this._tournamentRules.ScorePlayer.RunRule(this._tournament, game, game, this._games[typeTournament].Rules, ind);
        this._tournamentRules.ScoreTeam.RunRule(this._tournament, game, game, this._games[typeTournament].Rules, ind);
        this._tournamentRules.WinnerTournament.RunRule(this._tournament, game, game, this._games[typeTournament].Rules,
            ind);
    }

    /// <summary>
    /// Determinar cuando finaliza un torneo
    /// </summary>
    /// <returns>Si el torneo termino</returns>
    private bool EndTournament()
    {
        return this._tournament.TeamWinner != -1;
    }

    /// <summary>
    /// Determinar la distribucion de los jugadores
    /// </summary>
    /// <returns>Distribucion de los jugadores</returns>
    private List<(int, int,string)> DeterminatePlayerTeams()
    {
        List<(int, int,string)> aux = new List<(int, int,string)>();

        for (int i = 0; i < this._tournament.Teams.Count; i++)
        {
            if (this._tournament.ValidTeam[i])
            {
                foreach (var item in _tournament.Teams[i])
                {
                    if (this._tournament.ValidPlayer[item.Id]) aux.Add((this._tournament.Teams[i].Id, item.Id,item.Name));
                }
            }
        }

        return aux;
    }

    private void SortTeam(List<(int, int,string)> players)
    {
        players.Sort();

        int value = -1;
        int index = -1;
        for (int i = 0; i < players.Count; i++)
        {
            if (value != players[i].Item1)
            {
                index++;
                value = players[i].Item1;
            }

            players[i] = (index, i,players[i].Item3);
        }
    }
}