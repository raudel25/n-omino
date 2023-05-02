namespace Table;

public abstract class TableGame<T> : IReset<TableGame<T>>, ICloneable<TableGame<T>>
{
    protected TableGame(int dimension)
    {
        PlayNode = new HashSet<INode<T>>();
        FreeNode = new HashSet<INode<T>>();
        TableNode = new List<INode<T>>();
        DimensionToken = dimension;
    }

    /// <summary>Nodos que contienen una ficha</summary>
    public HashSet<INode<T>> PlayNode { get; protected set; }

    /// <summary>Nodos disponibles para jugar</summary>
    public HashSet<INode<T>> FreeNode { get; protected set; }

    /// <summary>Nodos contenidos en el grafo</summary>
    public List<INode<T>> TableNode { get; protected set; }

    public int DimensionToken { get; protected set; }

    /// <summary>Clonar la mesa</summary>
    /// <returns>Mesa clonada</returns>
    public abstract TableGame<T> Clone();

    TableGame<T> IReset<TableGame<T>>.Reset()
    {
        return Reset();
    }

    /// <summary>Expandir un nodo</summary>
    /// <param name="node">Nodo que se desea expandir</param>
    protected abstract void Expand(INode<T> node);

    /// <summary>Colocar una ficha en el tablero</summary>
    /// <param name="node">Nodo por el cual se juega la ficha</param>
    /// <param name="token">Ficha para jugar</param>
    /// <param name="values">Valores a asignar en el nodo</param>
    public void PlayTable(INode<T> node, Token<T> token, T[] values, int IdPlayer)
    {
        node.IdPlayer = IdPlayer;
        node.ValueToken = token;
        AssignFathers(node);
        AssignValues(node, values);
        Expand(node);
        PlayNode.Add(node);
        FreeNode.Remove(node);
    }

    /// <summary>Indica que un nodo esta libre para jugar</summary>
    /// <param name="node">Nodo para realizar la operacion</param>
    protected void FreeTable(INode<T> node)
    {
        if (PlayNode.Contains(node)) return;
        FreeNode.Add(node);
    }

    /// <summary>Recorrer el grafo</summary>
    /// <param name="node">Nodo inicial</param>
    /// <param name="ind">Arista del nodo al se quiere ir</param>
    /// <returns>Node vecino al cual se realiza el movimiento</returns>
    public INode<T> MoveTable(INode<T> node, int ind)
    {
        return node.Connections[ind]!;
    }

    /// <summary>Unir dos nodos</summary>
    /// <param name="right">Nodo para realizar la union</param>
    /// <param name="left">Nodo para realizar la union</param>
    /// <param name="ind">Arista por la que los nodos se conectan</param>
    protected virtual void UnionNode(INode<T> left, INode<T> right, int ind)
    {
        right.Connections[ind] = left;
        left.Connections[ind] = right;
    }

    /// <summary>
    ///     Asignar lor padres a los nodos
    /// </summary>
    /// <param name="node">Nodo para asignar los padres</param>
    protected void AssignFathers(INode<T> node)
    {
        for (var i = 0; i < node.Connections.Length; i++)
            if (node.Connections[i] != null)
                if (PlayNode.Contains(node.Connections[i]!))
                    node.Fathers.Add(node.Connections[i]!);
    }

    /// <summary>Clonar la mesa</summary>
    /// <param name="table">Nueva mesa que se va a retornar como copia</param>
    /// <returns>Mesa clonada</returns>
    protected TableGame<T> AuxClone(TableGame<T> table)
    {
        foreach (var item in PlayNode)
        {
            var valuesAux = new T[item.ValuesConnections.Length];
            Array.Copy(item.ValuesConnections, valuesAux, item.ValuesConnections.Length);
            table.PlayTable(table.TableNode[item.Id], item.ValueToken.Clone(), valuesAux, item.IdPlayer);
        }

        return table;
    }

    /// <summary>Asignar los valores correspondietes a las conexiones</summary>
    /// <param name="node">Nodo al que asignar los valores</param>
    /// <param name="values">Valores a asignar</param>
    protected abstract void AssignValues(INode<T> node, T[] values);

    public abstract TableGame<T> Reset();

    /// <summary>
    ///     Determinar el valor de las conexiones del nodo
    /// </summary>
    /// <param name="node">Nodo a determinar</param>
    /// <param name="ind">Indice de la conexion</param>
    /// <returns></returns>
    public abstract ValuesNode<T>? ValuesNodeTable(INode<T> node, int ind);
}