namespace Table;

public class TableDimension<T> : TableGame<T>, ICloneable<TableGame<T>>
{
    /// <summary>Cantidad de conexiones de un nodo de la mesa</summary>
    public TableDimension(int n) : base(n)
    {
        var node = CreateNode(n);
        FreeTable(node);
    }

    public override TableGame<T> Clone()
    {
        TableGame<T> table = new TableDimension<T>(DimensionToken);
        return AuxClone(table);
    }

    protected override void Expand(INode<T> node)
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
    protected INode<T> CreateNode(int n)
    {
        INode<T> node = new NodeDimension<T>(n, TableNode.Count);
        TableNode.Add(node);
        return node;
    }

    protected override void AssignValues(INode<T> node, T[] values)
    {
        var nodeDimension = node as NodeDimension<T>;

        if (nodeDimension == null) return;

        Array.Copy(values, nodeDimension.ValuesConnections, values.Length);

        for (var i = 0; i < nodeDimension.ValuesAssign.Length; i++)
        {
            if (nodeDimension.ValuesAssign[i].IsAssignValue) continue;
            nodeDimension.ValuesAssign[i].IsAssignValue = true;
            nodeDimension.ValuesAssign[i].Values.Add(values[i]);
        }
    }

    /// <summary>Asignar los mismos valores a 2 nodos conectados</summary>
    /// <param name="node">Nodo conectado</param>
    /// <param name="nodeConnection">Nodo conectado</param>
    /// <param name="ind">Indice de los nodos conectados</param>
    protected void AssignValueConnection(INode<T> node, INode<T> nodeConnection, int ind)
    {
        var nodeDimension = node as NodeDimension<T>;
        var nodeDimensionConnect = nodeConnection as NodeDimension<T>;
        if (nodeDimension == null || nodeDimensionConnect == null) return;
        // nodeDimensionConnect.ValuesConnections[ind] = nodeDimension.ValuesConnections[ind];
        nodeDimensionConnect.ValuesAssign[ind].IsAssignValue = true;
        nodeDimensionConnect.ValuesAssign[ind].Values.Add(nodeDimension.ValuesConnections[ind]);
    }

    public override ValuesNode<T>? ValuesNodeTable(INode<T> node, int ind)
    {
        var nodeDimension = node as NodeDimension<T>;

        if (nodeDimension == null) return null;

        return nodeDimension.ValuesAssign[ind];
    }

    public override TableGame<T> Reset()
    {
        return new TableDimension<T>(DimensionToken);
    }
}