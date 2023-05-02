namespace Table;

public class NodeDimension<T> : INode<T>
{
    public NodeDimension(int n, int id)
    {
        ValueToken = null!;
        ValuesConnections = new T[n];
        ValuesAssign = new ValuesNode<T>[n];
        Connections = new INode<T>[n];
        Id = id;
        Fathers = new List<INode<T>>();

        //Asiganar valores
        for (var i = 0; i < ValuesAssign.Length; i++) ValuesAssign[i] = new ValuesNode<T>();
    }

    /// <summary>
    ///     Determinar si el valor de una conexion ya fue asignado
    /// </summary>
    public ValuesNode<T>[] ValuesAssign { get; }

    /// <summary>
    ///     Primera conexion ocupada
    /// </summary>
    public int FirstConnection => Connection();

    public Token<T> ValueToken { get; set; }
    public INode<T>?[] Connections { get; }
    public T[] ValuesConnections { get; }

    public int Id { get; }
    int INode<T>.IdPlayer { get; set; }
    public List<INode<T>> Fathers { get; }

    /// <summary>
    ///     Deteminar la primera conexion ocupada
    /// </summary>
    /// <returns>Indice de la primera conexion ocupada</returns>
    private int Connection()
    {
        for (var i = 0; i < Connections.Length; i++)
            if (ValuesAssign[i].IsAssignValue)
                return i;

        return -1;
    }
}