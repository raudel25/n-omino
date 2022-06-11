namespace Table;

public class NodeDimension : INode
{
    public Token ValueToken { get; set; }
    public INode?[] Connections { get; set; }
    public int[] ValuesConnections { get; set; }
    public int Id { get; private set; }
    public int IdPlayer { get; set; }

    public int FirstConnectionFree
    {
        get { return ConnectionFree(); }
    }

    public int FirstConnection
    {
        get { return Connection(); }
    }

    private int ConnectionFree()
    {
        for (int i = 0; i < this.Connections.Length; i++)
        {
            if (this.Connections[i] == null) return i;
        }

        return -1;
    }

    private int Connection()
    {
        for (int i = 0; i < this.Connections.Length; i++)
        {
            if (this.Connections[i] != null) return i;
        }

        return -1;
    }

    public NodeDimension(int n, int id)
    {
        this.ValueToken = null!;
        this.ValuesConnections = new int[n];
        for (int i = 0; i < n; i++)
        {
            this.ValuesConnections[i] = -1;
        }

        this.Connections = new INode[n];
        this.Id = id;
    }
}