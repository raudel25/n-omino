namespace Table;

public class TableDimension : TableGame
{
    public override HashSet<Node> PlayNode { get; protected set; }
    public override HashSet<Node> FreeNode { get; protected set; }
    public override List<Node> TableNode { get; protected set; }
    public TableDimension(Token token)
    {
        this.PlayNode = new HashSet<Node>();
        this.FreeNode = new HashSet<Node>();
        this.TableNode = new List<Node>();
        Node node = CreateNode(token.CantValues);
        node.ValueToken = token;
        this.AsignValues(node, token.Values);
        this.Expand(node);
        this.PlayNode.Add(node);
    }
    protected override void Expand(Node node)
    {
        for (int i = 0; i < node.Conections.Length; i++)
        {
            if (node.Conections[i] == null)
            {
                this.UnionNode(node, CreateNode(node.Conections.Length), i);
                this.AsignValueConection(node, node.Conections[i], i);
                this.FreeTable(node.Conections[i]);
            }
        }
    }
    /// <summary>Crear un nodo</summary>
    /// <param name="n">Numero de aristas</param>
    /// <returns>Nuevo nodo</returns>
    protected Node CreateNode(int n)
    {
        Node node = new NodeDimension(n);
        this.TableNode.Add(node);
        return node;
    }
    protected override void AsignValues(Node node, int[] values)
    {
        NodeDimension nodeDimension = (node as NodeDimension)!;
        if (nodeDimension == null) return;
        int ind = 0;
        int conection = 0;
        bool find = false;
        //Buscamos si hay algun valor ya predeterminado
        for (int i = 0; i < nodeDimension.ValuesConections.Length; i++)
        {
            if (nodeDimension.ValuesConections[i] != -1)
            {
                conection = nodeDimension.ValuesConections[i];
                ind = i;
                find = true;
                break;
            }
        }
        //Asignamos los valores
        for (int i = 0; i < values.Length; i++)
        {
            if (find && values[i] == conection)
            {
                //Realizamos el cambio correspondiente con el valor preasignado
                nodeDimension.ValuesConections[i] = values[ind];
                find = false;
                continue;
            }
            if (nodeDimension.ValuesConections[i] == -1)
            {
                nodeDimension.ValuesConections[i] = values[i];
            }
        }
    }
    /// <summary>Asignar los mismos valores a 2 nodos conectados</summary>
    /// <param name="node">Nodo conectado</param>
    /// <param name="nodeConection">Nodo conectado</param>
    /// <param name="ind">Indice de los nodos conectados</param>
    protected void AsignValueConection(Node node, Node nodeConection, int ind)
    {
        NodeDimension nodeDimension = (node as NodeDimension)!;
        NodeDimension nodeDimensionConect = (nodeConection as NodeDimension)!;
        if (nodeDimension == null || nodeDimensionConect == null) return;
        nodeDimensionConect.ValuesConections[ind] = nodeDimension.ValuesConections[ind];
    }
}
