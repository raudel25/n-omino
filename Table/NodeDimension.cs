namespace Table;

public class NodeDimension : INode
{
    public Token ValueToken { get; set; }
    public INode?[] Conections { get; set; }
    public int[] ValuesConections { get; set; }
    public int Id { get; private set; }

    public int FirstConectionFree
    {
        get { return ConectionFree(); }
    }

    public int FirstConection
    {
        get { return Conection(); }
    }

    private int ConectionFree()
    {
        for (int i = 0; i < this.Conections.Length; i++)
        {
            if (this.Conections[i] == null) return i;
        }

        return -1;
    }

    private int Conection()
    {
        for (int i = 0; i < this.Conections.Length; i++)
        {
            if (this.Conections[i] != null) return i;
        }

        return -1;
    }

    public NodeDimension(int n, int id)
    {
        this.ValueToken = null!;
        this.ValuesConections = new int[n];
        for (int i = 0; i < n; i++)
        {
            this.ValuesConections[i] = -1;
        }

        this.Conections = new INode[n];
        this.Id = id;
    }
}