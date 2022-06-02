namespace Table;

public abstract class Node
{
    /// <summary>Valor de la ficha contenida en el nodo</summary>
    public abstract Token ValueToken { get; set; }
    /// <summary>Nodos vecinos</summary>
    public abstract Node[] Conections { get; set; }
    /// <summary>Valor de las conexiones del nodo</summary>
    public abstract int[] ValuesConections { get; set; }
    public int ID { get; set; }
}