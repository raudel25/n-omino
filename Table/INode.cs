namespace Table;

public interface INode
{
    /// <summary>Nodos vecinos</summary>
    public INode?[] Connections { get; }

    /// <summary>Valor de la ficha contenida en el nodo</summary>
    public Token ValueToken { get; set; }

    /// <summary>Valor de las conexiones del nodo</summary>
    public int[] ValuesConnections { get; }

    /// <summary>
    /// ID del Nodo
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// ID del jugador que jugo por el nodo
    /// </summary>
    public int IdPlayer { get; }
}