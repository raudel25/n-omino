using Table;

namespace InfoGame;

public class InfoPlayerTournament : ICloneable<InfoPlayerTournament>
{
    public InfoPlayerTournament(string name, int id)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    ///     Cantidad de juegos que ha ganado el jugador
    /// </summary>
    public int GamesToWin { get; set; }

    /// <summary>
    ///     Score del jugador
    /// </summary>
    public double ScoreTournament { get; set; }

    /// <summary>
    ///     ID del jugador
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Nombre del jugador
    /// </summary>
    public string Name { get; }

    public InfoPlayerTournament Clone()
    {
        return new InfoPlayerTournament(Name, Id);
    }
}