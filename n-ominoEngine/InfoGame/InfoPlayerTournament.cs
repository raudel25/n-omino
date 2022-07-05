namespace InfoGame;

public class InfoPlayerTournament
{
    /// <summary>
    /// Cantidad de juegos que ha ganado el jugador
    /// </summary>
    public int GamesToWin { get; set; }
    /// <summary>
    /// Score del jugador
    /// </summary>
    public int ScoreTournament { get; set; }
    /// <summary>
    /// ID del jugador
    /// </summary>
    public int Id { get; private set; }

    public InfoPlayerTournament(int id)
    {
        this.Id = id;
    }
}