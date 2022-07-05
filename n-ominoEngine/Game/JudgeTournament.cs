using InfoGame;
using Player;
using Rules;
using InteractionGui;

namespace Game;

public class JudgeTournament
{
    /// <summary>
    /// Inicializadores de juego
    /// </summary>
    private List<InitializerGame<dynamic>> _games;
    /// <summary>
    /// Reglas de juego
    /// </summary>
    private List<InfoRules<dynamic>> _rules;
    /// <summary>
    /// Jugadores
    /// </summary>
    private List<Player<dynamic>> _playersPlay;
    /// <summary>
    /// Printiador del juego
    /// </summary>
    private List<Printer> _print;
    /// <summary>
    /// Estado del torneo
    /// </summary>
    private TournamentStatus _tournament;
    /// <summary>
    /// Reglas del torneo
    /// </summary>
    private InfoRulesTournament<dynamic> _tournamentRules;

    public JudgeTournament(List<InitializerGame<dynamic>> games, List<InfoRules<dynamic>> rules, List<Player<dynamic>> playersPlay,
        List<Printer> print,
        List<(int,int)> playerTeams,InfoRulesTournament<dynamic> rulesTournament)
    {
        this._games = games;
        this._rules = rules;
        this._playersPlay = playersPlay;
        this._print = print;
        this._tournament = CreateTournament(playerTeams);
        this._tournamentRules = rulesTournament;
    }

    /// <summary>
    /// Crear un torneo
    /// </summary>
    /// <param name="playerTeams">Distribucion de jugadores</param>
    /// <returns>Estado del torneo</returns>
    private TournamentStatus CreateTournament(List<(int,int)> playerTeams)
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
            teams[teams.Count-1].Add(aux);
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
                PreGame(ind,i);
                
                List<(int,int)> playerTeams = DeterminatePlayerTeams();

                //Crear los players para usar en el juego
                Player<dynamic>[] players = new Player<dynamic>[playerTeams.Count];

                for (int j = 0; j < players.Length; j++)
                {
                    players[j] = this._playersPlay[playerTeams[j].Item2];
                }

                GameStatus<dynamic> init = this._games[i].StartGame(playerTeams);

                //Desarrollar un juego
                Judge<dynamic> judge = new Judge<dynamic>(this._tournament, this._rules[i], init, players,
                    this._print[i]);
                judge.Game();
                
                PostGame(ind,i,init);
                
                if(EndTournament()) break;

                ind++;
            }
        }
    }

    /// <summary>
    /// Analizar las reglas a ejecutar antes del juego
    /// </summary>
    /// <param name="ind">Indice del torneo</param>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    private void PreGame(int ind,int typeTournament)
    {
        this._tournamentRules.PlayerGame.RunRule(this._tournament,null!,null!,this._rules[typeTournament],ind);
        this._tournamentRules.TeamGame.RunRule(this._tournament,null!,null!,this._rules[typeTournament],ind);
    }

    /// <summary>
    /// Analizar las reglas a ejecutar despues del juego
    /// </summary>
    /// <param name="ind">Indice del torneo</param>
    /// <param name="typeTournament">Indice de las reglas del torneo</param>
    /// <param name="game">Estado del juego</param>
    private void PostGame(int ind, int typeTournament, GameStatus<dynamic> game)
    {
        this._tournamentRules.ScorePlayer.RunRule(this._tournament,game,game,this._rules[typeTournament],ind);
        this._tournamentRules.ScoreTeam.RunRule(this._tournament,game,game,this._rules[typeTournament],ind);
        this._tournamentRules.WinnerTournament.RunRule(this._tournament,game,game,this._rules[typeTournament],ind);
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
    private List<(int,int)> DeterminatePlayerTeams()
    {
        List<(int, int)> aux = new List<(int, int)>();
        
        for (int i = 0; i < this._tournament.Teams.Count; i++)
        {
            if (this._tournament.ValidTeam[i])
            {
                foreach (var item in _tournament.Teams[i])
                {
                    if(this._tournament.ValidPlayer[item.Id]) aux.Add((this._tournament.Teams[i].Id,item.Id));   
                }
            }
        }

        return aux;
    }
}