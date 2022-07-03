namespace InteractionGui;

public class InfoPlayerGui
{
    public double Score { get; private set; }
    
    public string Name { get; private set; }
    
    public int Passes { get; private set; }

    public InfoPlayerGui(string name, int passes, double score)
    {
        this.Name = name;
        this.Passes = passes;
        this.Score = score;
    }
}