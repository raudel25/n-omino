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
        List<int>[] teams,InfoRulesTournament<dynamic> rulesTournament)
    {
        this._games = games;
        this._rules = rules;
        this._playersPlay = playersPlay;
        this._print = print;
        this._tournament = CreateTournament(teams);
        this._tournamentRules = rulesTournament;
    }

    private TournamentStatus CreateTournament(List<int>[] team)
    {
        InfoPlayerTournament[] players = new InfoPlayerTournament[this._playersPlay.Length];
        List<InfoPlayerTournament>[] teamsPlayer = new List<InfoPlayerTournament>[team.Length];

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new InfoPlayerTournament(i);
        }

        for (int i = 0; i < teamsPlayer.Length; i++)
        {
            teamsPlayer[i] = new List<InfoPlayerTournament>();
            for (int j = 0; j < team[i].Count; j++)
            {
                teamsPlayer[i].Add(players[team[i][j]]);
            }
        }

        return new TournamentStatus(players, teamsPlayer);
    }

    public void TournamentGame()
    {
        int ind = 0;
        while (EndTournament())
        {
            for (int i = 0; i < this._games.Length; i++)
            {
                PreGame(ind,i);
                
                List<int> playersInfo = DeterminatePlayer();
                List<int>[] teams = DeterminateTeams();

                //Crear los players para usar en el juego
                Player<dynamic>[] players = new Player<dynamic>[playersInfo.Count];

                for (int j = 0; j < players.Length; j++)
                {
                    players[j] = this._playersPlay[playersInfo[j]];
                }

                GameStatus<dynamic> init = this._games[i].StartGame(playersInfo, teams);

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
    private List<int> DeterminatePlayer()
    {
        List<int> aux = new List<int>();

        for (int i = 0; i < this._tournament.ValidPlayer.Length; i++)
        {
            if (this._tournament.ValidPlayer[i]) aux.Add(i);
        }

        return aux;
    }

    private List<int>[] DeterminateTeams()
    {
        List<int>[] aux = new List<int>[this._tournament.Teams.Length];

        for (int i = 0; i < aux.Length; i++)
        {
            aux[i] = new List<int>();
            for (int j = 0; j < this._tournament.Teams[i].Count; j++)
            {
                if (this._tournament.ValidPlayer[this._tournament.Teams[i][j].Id])
                    aux[i].Add(this._tournament.Teams[i][j].Id);
            }
        }

        return aux;
    }
}