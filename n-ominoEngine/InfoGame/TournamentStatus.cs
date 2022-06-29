namespace InfoGame;

public class TournamentStatus
{
    public InfoPlayerTournament[] Players { get; set; }
    public List<InfoPlayerTournament>[] Teams { get; set; }
    public int[] ScoreTeams { get; set; }
    public bool[] ValidPlayer { get; set; }
    public int ImmediateWinner { get; set; }
    public int ImmediateWinnerTeam { get; set; }
    public int Index { get; set; }
    
    public int TeamWinner { get; set; }

    public TournamentStatus(InfoPlayerTournament[] players, List<InfoPlayerTournament>[] teams)
    {
        this.Players = players;
        this.Teams = teams;
        this.ValidPlayer = new bool[players.Length];
        this.ScoreTeams = new int[teams.Length];
        this.TeamWinner = -1;
    }
}