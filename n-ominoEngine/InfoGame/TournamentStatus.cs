namespace InfoGame;

public class TournamentStatus
{
    /// <summary>
    /// Lista de jugadores
    /// </summary>
    public List<InfoPlayerTournament> Players { get; set; }
    /// <summary>
    /// Lista de equipos
    /// </summary>
    public List<InfoTeams<InfoPlayerTournament>> Teams { get; set; }
    /// <summary>
    /// Score de los equipos
    /// </summary>
    public double[] ScoreTeams { get; set; }
    /// <summary>
    /// Determinar los jugadores que pueden jugar
    /// </summary>
    public bool[] ValidPlayer { get; set; }
    /// <summary>
    /// Deteminar los equipos que pueden jugar
    /// </summary>
    public bool[] ValidTeam { get; set; }
    /// <summary>
    /// Ganador inmediato de un juego
    /// </summary>
    public int ImmediateWinner { get; set; }
    /// <summary>
    /// Equipo ganador inmediato del juego
    /// </summary>
    public int ImmediateWinnerTeam { get; set; }
    /// <summary>
    /// Indice del juego actual
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// Equipo ganador del torneo
    /// </summary>
    public int TeamWinner { get; set; }

    public List<(int, int)>? DistributionPlayers { get; set; }

    public TournamentStatus(List<InfoPlayerTournament> players, List<InfoTeams<InfoPlayerTournament>> teams)
    {
        this.Players = players;
        this.Teams = teams;
        this.ValidPlayer = new bool[players.Count];
        this.ScoreTeams = new double[teams.Count];
        this.TeamWinner = -1;
        this.ValidTeam = new bool[teams.Count];
    }
}