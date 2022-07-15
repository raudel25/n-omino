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
        List<(int, int)> playerTeams, InfoRulesTournament<T> rulesTournament)
    {
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
    private TournamentStatus CreateTournament(List<(int, int)> playerTeams)
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

            var aux = new InfoPlayerTournament(item.Item2);
            teams[teams.Count - 1].Add(aux);
            players.Add(aux);
        }

        return new TournamentStatus(players, teams);
    }

    public void TournamentGame()
    {
        int ind = 0;
        while (EndTournament())
        {
            for (int i = 0; i < this._games.Count; i++)
            {
                PreGame(ind, i);

                List<(int, int)> playerTeams = DeterminatePlayerTeams();

                //Crear los players para usar en el juego
                List<Player<T>> players = new List<Player<T>>();

                for (int j = 0; j < players.Count; j++)
                {
                    players.Add(this._playersPlay[playerTeams[j].Item2]);
                }

                GameStatus<T> init = this._games[i].Initializer.StartGame(playerTeams);

                //Desarrollar un juego
                Judge<T> judge = new Judge<T>(this._tournament, this._games[i].Rules, init, players,
                    this._games[i].Print);
                judge.Game();

                PostGame(ind, i, init);

                if (EndTournament()) break;

                ind++;
            }
        }
    }

    /// <summary>
    /// Analizar las reglas a ejecutar antes del juego
    /// </summary>
    /// <param name="ind">Indice del torneo</param>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    private void PreGame(int ind, int typeTournament)
    {
        this._tournamentRules.PlayerGame.RunRule(this._tournament, null!, null!, this._games[typeTournament].Rules, ind);
        this._tournamentRules.TeamGame.RunRule(this._tournament, null!, null!, this._games[typeTournament].Rules, ind);
    }

    /// <summary>
    /// Analizar las reglas a ejecutar despues del juego
    /// </summary>
    /// <param name="ind">Indice del torneo</param>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    /// <param name="game">Estado del juego</param>
    private void PostGame(int ind, int typeTournament, GameStatus<T> game)
    {
        this._tournamentRules.ScorePlayer.RunRule(this._tournament, game, game, this._games[typeTournament].Rules, ind);
        this._tournamentRules.ScoreTeam.RunRule(this._tournament, game, game, this._games[typeTournament].Rules, ind);
        this._tournamentRules.WinnerTournament.RunRule(this._tournament, game, game, this._games[typeTournament].Rules, ind);
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
    private List<(int, int)> DeterminatePlayerTeams()
    {
        List<(int, int)> aux = new List<(int, int)>();

        for (int i = 0; i < this._tournament.Teams.Count; i++)
        {
            if (this._tournament.ValidTeam[i])
            {
                foreach (var item in _tournament.Teams[i])
                {
                    if (this._tournament.ValidPlayer[item.Id]) aux.Add((this._tournament.Teams[i].Id, item.Id));
                }
            }
        }

        return aux;
    }
}