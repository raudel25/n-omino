namespace Game;

public class InfoPlayerGui
{
    public InfoPlayerGui(string name, int passes, double score)
    {
        Name = name;
        Passes = passes;
        Score = score;
    }

    /// <summary>
    ///     Score del jugador
    /// </summary>
    public double Score { get; }

    /// <summary>
    ///     Nombre del jugador
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Cantidad de pases
    /// </summary>
    public int Passes { get; }
}