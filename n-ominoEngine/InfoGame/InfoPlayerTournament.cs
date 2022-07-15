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
    public double ScoreTournament { get; set; }
    /// <summary>
    /// ID del jugador
    /// </summary>
    public int Id { get; private set; }
    
    /// <summary>
    /// Nombre del jugador
    /// </summary>
    public string Name { get; private set; }

    public InfoPlayerTournament(string name,int id)
    {
        this.Id = id;
        this.Name = name;
    }
}