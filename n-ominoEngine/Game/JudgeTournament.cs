using InfoGame;
using Player;
using Rules;
using InteractionGui;

namespace Game;

public class JudgeTournament
{
    private InitializerGame<dynamic>[] _games;
    private InfoRules<dynamic>[] _rules;
    private Player<dynamic>[] _playersPlay;
    private Printer[] _print;
    private TournamentStatus _tournament;
    private InfoRulesTournament<dynamic> _tournamentRules;

    public JudgeTournament(InitializerGame<dynamic>[] games, InfoRules<dynamic>[] rules, Player<dynamic>[] playersPlay,
        Printer[] print,
        List<(int,int)> playerTeams,InfoRulesTournament<dynamic> rulesTournament)
    {
        this._games = games;
        this._rules = rules;
        this._playersPlay = playersPlay;
        this._print = print;
        this._tournament = CreateTournament(playerTeams);
        this._tournamentRules = rulesTournament;
    }

    private TournamentStatus CreateTournament(List<(int,int)> playerTeams)
    {
        List<InfoPlayerTournament> players = new List<InfoPlayerTournament>();
        List<InfoTeams<InfoPlayerTournament>> teams = new List<InfoTeams<InfoPlayerTournament>>();

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
            for (int i = 0; i < this._games.Length; i++)
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

                Judge<dynamic> judge = new Judge<dynamic>(this._tournament, this._rules[i], init, players,
                    this._print[i]);
                judge.Game();
                
                PostGame(ind,i,init);

                ind++;
            }
        }
    }

    private void PreGame(int ind,int typeTournament)
    {
        this._tournamentRules.PlayerGame.RunRule(this._tournament,null!,null!,this._rules[typeTournament],ind);
        this._tournamentRules.TeamGame.RunRule(this._tournament,null!,null!,this._rules[typeTournament],ind);
    }

    private void PostGame(int ind, int typeTournament, GameStatus<dynamic> game)
    {
        this._tournamentRules.ScorePlayer.RunRule(this._tournament,game,game,this._rules[typeTournament],ind);
        this._tournamentRules.ScoreTeam.RunRule(this._tournament,game,game,this._rules[typeTournament],ind);
        this._tournamentRules.WinnerTournament.RunRule(this._tournament,game,game,this._rules[typeTournament],ind);
    }

    private bool EndTournament()
    {
        return this._tournament.TeamWinner != -1;
    }
    
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