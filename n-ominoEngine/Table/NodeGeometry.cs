namespace Table;

public class NodeGeometry<T> : INode<T>
{
    public readonly Coordinates Location;

    public NodeGeometry((int, int)[] connections, int id)
    {
        ValuesConnections = new T[connections.Length];
        Connections = new INode<T>[connections.Length];
        Location = new Coordinates(connections);
        ValueToken = null!;
        Id = id;
        Fathers = new List<INode<T>>();
    }

    public Token<T> ValueToken { get; set; }
    public INode<T>[] Connections { get; }
    public T[] ValuesConnections { get; }
    public int Id { get; }
    int INode<T>.IdPlayer { get; set; }
    public List<INode<T>> Fathers { get; }

    public override bool Equals(object? obj)
    {
        var aux = obj as NodeGeometry<T>;
        if (aux == null) return false;
        return Location.Equals(aux.Location);
    }

    public override int GetHashCode()
    {
        return Location.GetHashCode();
    }
}