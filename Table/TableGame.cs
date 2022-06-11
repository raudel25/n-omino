namespace Table;

public abstract class TableGame
{
    /// <summary>Nodos que contienen una ficha</summary>
    public HashSet<INode> PlayNode { get; protected set; }

    /// <summary>Nodos disponibles para jugar</summary>
    public HashSet<INode> FreeNode { get; protected set; }

    /// <summary>Nodos contenidos en el grafo</summary>
    public List<INode> TableNode { get; protected set; }

    protected TableGame()
    {
        this.PlayNode = new HashSet<INode>();
        this.FreeNode = new HashSet<INode>();
        this.TableNode = new List<INode>();
    }

    /// <summary>Expandir un nodo</summary>
    /// <param name="node">Nodo que se desea expandir</param>
    protected abstract void Expand(INode node);

    /// <summary>Colocar una ficha en el tablero</summary>
    /// <param name="node">Nodo por el cual se juega la ficha</param>
    /// <param name="token">Ficha para jugar</param>
    /// <param name="values">Valores a asignar en el nodo</param>
    public void PlayTable(INode node, Token token, int[] values)
    {
        node.ValueToken = token;
        this.AssignValues(node, values);
        this.Expand(node);
        this.PlayNode.Add(node);
        FreeNode.Remove(node);
    }

    /// <summary>Indica que un nodo esta libre para jugar</summary>
    /// <param name="node">Nodo para realizar la operacion</param>
    public void FreeTable(INode node)
    {
        if (this.PlayNode.Contains(node)) return;
        this.FreeNode.Add(node);
    }

    /// <summary>Recorrer el grafo</summary>
    /// <param name="node">Nodo inicial</param>
    /// <param name="ind">Arista del nodo al se quiere ir</param>
    /// <returns>Node vecino al cual se realiza el movimiento</returns>
    public INode MoveTable(INode node, int ind)
    {
        return node.Connections[ind]!;
    }

    /// <summary>Unir dos nodos</summary>
    /// <param name="right">Nodo para realizar la union</param>
    /// <param name="left">Nodo para realizar la union</param>
    /// <param name="ind">Arista por la que los nodos se conectan</param>
    protected void UnionNode(INode right, INode left, int ind)
    {
        right.Connections[ind] = left;
        left.Connections[ind] = right;
    }

    /// <summary>Clonar la mesa</summary>
    /// <param name="table">Nueva mesa que se va a retornar como copia</param>
    /// <returns>Mesa clonada</returns>
    protected TableGame AuxClone(TableGame table)
    {
        foreach (var item in this.PlayNode)
        {
            int[] valuesAux = new int[item.ValuesConnections.Length];
            Array.Copy(item.ValuesConnections, valuesAux, item.ValuesConnections.Length);
            table.PlayTable(table.TableNode[item.Id], item.ValueToken.Clone(), valuesAux);
        }

        return table;
    }

    /// <summary>Asignar los valores correspondietes a las conexiones</summary>
    /// <param name="node">Nodo al que asignar los valores</param>
    /// <param name="values">Valores a asignar</param>
    protected abstract void AssignValues(INode node, int[] values);

    /// <summary>Clonar la mesa</summary>
    /// <returns>Mesa clonada</returns>
    public abstract TableGame Clone();
}