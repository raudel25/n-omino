namespace Table;

public abstract class TableGame
{
    /// <summary>Nodos que contienen una ficha</summary>
    public abstract HashSet<Node> PlayNode { get; protected set; }
    /// <summary>Nodos disponibles para jugar</summary>
    public abstract HashSet<Node> FreeNode { get; protected set; }
    /// <summary>Nodos contenidos en el grafo</summary>
    public abstract List<Node> TableNode { get; protected set; }
    /// <summary>Expandir un nodo</summary>
    /// <param name="node">Nodo que se desea expandir</param>
    protected abstract void Expand(Node node);
    /// <summary>Colocar una ficha en el tablero</summary>
    /// <param name="node">Nodo por el cual se juega la ficha</param>
    public void PlayTable(Node node, Token token, int[] values)
    {
        node.ValueToken = token;
        this.AsignValues(node, values);
        this.Expand(node);
        this.PlayNode.Add(node);
        FreeNode.Remove(node);
    }
    /// <summary>Asignar los valores correspondietes a las conexiones</summary>
    /// <param name="node">Nodo al que asignar los valores</param>
    /// <param name="values">Valores a asignar</param>
    protected abstract void AsignValues(Node node, int[] values);
    /// <summary>Indica que un nodo esta libre para jugar</summary>
    /// <param name="node">Nodo para realizar la operacion</param>
    public void FreeTable(Node node)
    {
        if (this.PlayNode.Contains(node)) return;
        this.FreeNode.Add(node);
    }
    /// <summary>Recorrer el grafo</summary>
    /// <param name="node">Nodo inicial</param>
    /// <param name="ind"Arista del nodo al se quiere ir</param>
    /// <returns>Node vecino al cual se realiza el movimiento</returns>
    public Node MoveTable(Node node, int ind)
    {
        return node.Conections[ind];
    }
    /// <summary>Unir dos nodos</summary>
    /// <param name="right">Nodo para realizar la union</param>
    /// <param name="left">Nodo para realizar la union</param>
    /// <param name="ind">Arista por la que los nodos se conectan</param>
    protected void UnionNode(Node right, Node left, int ind)
    {
        right.Conections[ind] = left;
        left.Conections[ind] = right;
    }
    /// <summary>Clonar la mesa</summary>
    /// <param name="table">Nueva mesa que se va a retornar como copia</param>
    /// <returns>Mesa clonada</returns>
    protected TableGame AuxClone(TableGame table)
    {
        foreach (var item in this.PlayNode)
        {
            int[] valuesAux = new int[item.ValuesConections.Length];
            Array.Copy(item.ValuesConections, valuesAux, item.ValuesConections.Length);
            table.PlayTable(table.TableNode[item.ID], item.ValueToken.Clone(), valuesAux);
        }
        return table;
    }
    /// <summary>Clonar la mesa</summary>
    /// <returns>Mesa clonada</returns>
    public abstract TableGame Clone();
}