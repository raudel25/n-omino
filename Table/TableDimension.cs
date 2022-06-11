namespace Table;

public class TableDimension : TableGame
{
    /// <summary>Cantidad de conexiones de un nodo de la mesa</summary>
    public int Dimension { get; protected set; }

    public TableDimension(int n)
    {
        Dimension = n;
        var node = CreateNode(n);
        FreeTable(node);
    }

    protected override void Expand(INode node)
    {
        for (var i = 0; i < node.Connections.Length; i++)
            if (node.Connections[i] == null)
            {
                UnionNode(node, CreateNode(node.Connections.Length), i);
                AssignValueConnection(node, node.Connections[i]!, i);
                FreeTable(node.Connections[i]!);
            }
    }

    /// <summary>Crear un nodo</summary>
    /// <param name="n">Numero de aristas</param>
    /// <returns>Nuevo nodo</returns>
    protected INode CreateNode(int n)
    {
        INode node = new NodeDimension(n, TableNode.Count);
        TableNode.Add(node);
        return node;
    }

    protected override void AssignValues(INode node, int[] values)
    {
        var nodeDimension = node as NodeDimension;
        if (nodeDimension == null) return;
        Array.Copy(values, nodeDimension.ValuesConnections, values.Length);
    }

    /// <summary>Asignar los mismos valores a 2 nodos conectados</summary>
    /// <param name="node">Nodo conectado</param>
    /// <param name="nodeConnection">Nodo conectado</param>
    /// <param name="ind">Indice de los nodos conectados</param>
    protected void AssignValueConnection(INode node, INode nodeConnection, int ind)
    {
        var nodeDimension = node as NodeDimension;
        var nodeDimensionConnect = nodeConnection as NodeDimension;
        if (nodeDimension == null || nodeDimensionConnect == null) return;
        nodeDimensionConnect.ValuesConnections[ind] = nodeDimension.ValuesConnections[ind];
    }

    public override TableGame Clone()
    {
        TableGame table = new TableDimension(Dimension);
        return AuxClone(table);
    }
}