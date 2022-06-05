namespace Table;

public interface INode
{
    /// <summary>Nodos vecinos</summary>
    public INode?[] Conections { get;}
    /// <summary>Valor de la ficha contenida en el nodo</summary>
    public Token ValueToken { get; set; }
    /// <summary>Valor de las conexiones del nodo</summary>
    public int[] ValuesConections { get;}

    public int Id { get;}
}