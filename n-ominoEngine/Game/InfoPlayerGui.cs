namespace Game;

public class InfoPlayerGui
{
    /// <summary>
    /// Score del jugador
    /// </summary>
    public double Score { get; private set; }
    
    /// <summary>
    /// Nombre del jugador
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Cantidad de pases
    /// </summary>
    public int Passes { get; private set; }

    public InfoPlayerGui(string name, int passes, double score)
    {
        this.Name = name;
        this.Passes = passes;
        this.Score = score;
    }
}