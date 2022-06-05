namespace Table;

public class TableDimension : TableGame
{
    /// <summary>Cantidad de conexiones de un nodo de la mesa</summary>
    public int Dimension { get; protected set; }
    public TableDimension(int n)
    {
        this.Dimension = n;
        INode node = CreateNode(n);
        this.FreeTable(node);
    }
    protected override void Expand(INode node)
    {
        for (int i = 0; i < node.Conections.Length; i++)
        {
            if (node.Conections[i] == null)
            {
                this.UnionNode(node, CreateNode(node.Conections.Length), i);
                this.AsignValueConection(node, node.Conections[i]!, i);
                this.FreeTable(node.Conections[i]!);
            }
        }
    }
    /// <summary>Crear un nodo</summary>
    /// <param name="n">Numero de aristas</param>
    /// <returns>Nuevo nodo</returns>
    protected INode CreateNode(int n)
    {
        INode node = new NodeDimension(n,this.TableNode.Count);
        this.TableNode.Add(node);
        return node;
    }
    protected override void AsignValues(INode node, int[] values)
    {
        NodeDimension? nodeDimension = (node as NodeDimension);
        if (nodeDimension == null) return;
        Array.Copy(values, nodeDimension.ValuesConections, values.Length);
    }
    /// <summary>Asignar los mismos valores a 2 nodos conectados</summary>
    /// <param name="node">Nodo conectado</param>
    /// <param name="nodeConection">Nodo conectado</param>
    /// <param name="ind">Indice de los nodos conectados</param>
    protected void AsignValueConection(INode node, INode nodeConection, int ind)
    {
        NodeDimension? nodeDimension = (node as NodeDimension);
        NodeDimension? nodeDimensionConect = (nodeConection as NodeDimension);
        if (nodeDimension == null || nodeDimensionConect == null) return;
        nodeDimensionConect.ValuesConections[ind] = nodeDimension.ValuesConections[ind];
    }
    public override TableGame Clone()
    {
        TableGame table = new TableDimension(this.Dimension);
        return this.AuxClone(table);
    }
}
