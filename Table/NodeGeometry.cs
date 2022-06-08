namespace Table;

public class NodeGeometry : INode
{
    public Token ValueToken { get; set; }
    public INode[] Conections { get; set; }
    public int[] ValuesConections { get; set; }
    public readonly Coordenates Ubication;
    public int Id { get; private set; }

    public NodeGeometry((int, int)[] conections, int id)
    {
        this.ValuesConections = new int[conections.Length];
        this.Conections = new INode[conections.Length];
        this.Ubication = new Coordenates(conections);
        this.ValueToken = null!;
        this.Id = id;
    }

    public override bool Equals(object? obj)
    {
        NodeGeometry? aux = (obj as NodeGeometry);
        if (aux == null) return false;
        return this.Ubication.Equals(aux.Ubication);
    }

    public override int GetHashCode()
    {
        return this.Ubication.GetHashCode();
    }
}