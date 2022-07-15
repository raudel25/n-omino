namespace Table;

public class NodeGeometry<T> : INode<T>
{
    public Token<T> ValueToken { get; set; }
    public INode<T>[] Connections { get; private set; }
    public T[] ValuesConnections { get; private set; }
    public readonly Coordinates Location;
    public int Id { get; private set; }
    int INode<T>.IdPlayer { get; set; }
    public List<INode<T>> Fathers { get; private set; }

    public NodeGeometry((int, int)[] connections, int id)
    {
        this.ValuesConnections = new T[connections.Length];
        this.Connections = new INode<T>[connections.Length];
        this.Location = new Coordinates(connections);
        this.ValueToken = null!;
        this.Id = id;
        this.Fathers = new List<INode<T>>();
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