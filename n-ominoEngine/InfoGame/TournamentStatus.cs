using Table;

namespace InfoGame;

public class TournamentStatus : ICloneable<TournamentStatus>
{
    public TournamentStatus(List<InfoPlayerTournament> players, List<InfoTeams<InfoPlayerTournament>> teams)
    {
        Players = players;
        Teams = teams;
        ValidPlayer = new bool[players.Count];
        ScoreTeams = new double[teams.Count];
        TeamWinner = -1;
        ValidTeam = new bool[teams.Count];
        ImmediateWinner = -1;
        ImmediateWinnerTeam = -1;
    }

    /// <summary>
    ///     Lista de jugadores
    /// </summary>
    public List<InfoPlayerTournament> Players { get; set; }

    /// <summary>
    ///     Lista de equipos
    /// </summary>
    public List<InfoTeams<InfoPlayerTournament>> Teams { get; set; }

    /// <summary>
    ///     Score de los equipos
    /// </summary>
    public double[] ScoreTeams { get; set; }

    /// <summary>
    ///     Determinar los jugadores que pueden jugar
    /// </summary>
    public bool[] ValidPlayer { get; set; }

    /// <summary>
    ///     Deteminar los equipos que pueden jugar
    /// </summary>
    public bool[] ValidTeam { get; set; }

    /// <summary>
    ///     Ganador inmediato de un juego
    /// </summary>
    public int ImmediateWinner { get; set; }

    /// <summary>
    ///     Equipo ganador inmediato del juego
    /// </summary>
    public int ImmediateWinnerTeam { get; set; }

    /// <summary>
    ///     Indice del juego actual
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    ///     Equipo ganador del torneo
    /// </summary>
    public int TeamWinner { get; set; }

    public List<(int, int, string)>? DistributionPlayers { get; set; }

    public TournamentStatus Clone()
    {
        var players = new List<InfoPlayerTournament>();

        foreach (var item in Players) players.Add(item.Clone());

        var teams = new List<InfoTeams<InfoPlayerTournament>>();

        var index = 0;
        for (var i = 0; i < Teams.Count; i++)
        {
            teams.Add(new InfoTeams<InfoPlayerTournament>(Teams[i].Id));
            for (var j = 0; j < Teams[i].Count; j++)
            {
                teams[teams.Count - 1].Add(players[index]);
                index++;
            }
        }

        return new TournamentStatus(players, teams);
    }
}