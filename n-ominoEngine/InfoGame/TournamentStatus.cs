namespace InfoGame;

public class TournamentStatus
{
    public List<InfoPlayerTournament> Players { get; set; }
    public List<InfoTeams<InfoPlayerTournament>> Teams { get; set; }
    public int[] ScoreTeams { get; set; }
    public bool[] ValidPlayer { get; set; }
    
    public bool[] ValidTeam { get; set; }
    public int ImmediateWinner { get; set; }
    public int ImmediateWinnerTeam { get; set; }
    public int Index { get; set; }
    
    public int TeamWinner { get; set; }

    public TournamentStatus(List<InfoPlayerTournament> players, List<InfoTeams<InfoPlayerTournament>> teams)
    {
        this.Players = players;
        this.Teams = teams;
        this.ValidPlayer = new bool[players.Count];
        this.ScoreTeams = new int[teams.Count];
        this.TeamWinner = -1;
        this.ValidTeam = new bool[teams.Count];
    }
}