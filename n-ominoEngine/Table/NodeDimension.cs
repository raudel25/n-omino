namespace Table;

public class NodeDimension<T> : INode<T>
{
    public Token<T> ValueToken { get; set; }
    public INode<T>?[] Connections { get; set; }
    public T[] ValuesConnections { get; set; }
    /// <summary>
    /// Determinar si el valor de una conexion ya fue asignado
    /// </summary>
    public bool[] ValuesAssign { get; set; }
    public int Id { get; private set; }
    public int IdPlayer { get; set; }
    public List<INode<T>> Fathers { get; private set; }

    /// <summary>
    /// Primera conexion ocupada
    /// </summary>
    public int FirstConnection
    {
        get { return Connection(); }
    }

    /// <summary>
    /// Deteminar la primera conexion ocupada
    /// </summary>
    /// <returns>Indice de la primera conexion ocupada</returns>
    private int Connection()
    {
        for (int i = 0; i < this.Connections.Length; i++)
        {
            if (this.ValuesAssign[i]) return i;
        }

        return -1;
    }

    public NodeDimension(int n, int id)
    {
        this.ValueToken = null!;
        this.ValuesConnections = new T[n];
        this.ValuesAssign = new bool[n];
        this.Connections = new INode<T>[n];
        this.Id = id;
        this.Fathers = new List<INode<T>>();
    }
}