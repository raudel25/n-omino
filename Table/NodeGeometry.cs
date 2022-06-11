namespace Table;

public class NodeGeometry<T> : INode<T>
{
    public Token<T> ValueToken { get; set; }
    public INode<T>[] Connections { get; set; }
    public T[] ValuesConnections { get; set; }
    public readonly Coordinates Location;
    public int Id { get; private set; }
    public int IdPlayer { get; set; }

    public NodeGeometry((int, int)[] connections, int id)
    {
        this.ValuesConnections = new T[connections.Length];
        this.Connections = new INode<T>[connections.Length];
        this.Location = new Coordinates(connections);
        this.ValueToken = null!;
        this.Id = id;
    }

    public override bool Equals(object? obj)
    {
        NodeGeometry<T>? aux = (obj as NodeGeometry<T>);
        if (aux == null) return false;
        return this.Location.Equals(aux.Location);
    }

    public override int GetHashCode()
    {
        return this.Location.GetHashCode();
    }
}