namespace Table;

public interface INode<T>
{
    /// <summary>Nodos vecinos</summary>
    public INode<T>?[] Connections { get; }

    /// <summary>Valor de la ficha contenida en el nodo</summary>
    public Token<T> ValueToken { get; set; }

    /// <summary>Valor de las conexiones del nodo</summary>
    public T[] ValuesConnections { get; }

    /// <summary>
    /// ID del Nodo
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// ID del jugador que jugo por el nodo
    /// </summary>
    public int IdPlayer { get; }

    /// <summary>
    /// Lista de nodos que tenian una ficha al momento de jugar por el nodo actual
    /// </summary>
    public List<INode<T>> Fathers { get; }
}