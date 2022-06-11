namespace Table;

public class NodeGeometry : INode
{
    public Token ValueToken { get; set; }
    public INode[] Connections { get; set; }
    public int[] ValuesConnections { get; set; }
    public readonly Coordinates Location;
    public int Id { get; private set; }
    public int IdPlayer { get; set; }

    public NodeGeometry((int, int)[] connections, int id)
    {
        this.ValuesConnections = new int[connections.Length];
        this.Connections = new INode[connections.Length];
        this.Location = new Coordinates(connections);
        this.ValueToken = null!;
        this.Id = id;
    }

    public override bool Equals(object? obj)
    {
        NodeGeometry? aux = (obj as NodeGeometry);
        if (aux == null) return false;
        return this.Location.Equals(aux.Location);
    }

    public override int GetHashCode()
    {
        return this.Location.GetHashCode();
    }
}