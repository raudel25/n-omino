namespace Table;

public class TableLongana<T> : TableDimension<T>, ICloneable<TableGame<T>>
{
    /// <summary>
    /// Cantidad de jugadores
    /// </summary>
    public int CantPlayer { get; private set; }

    /// <summary>
    /// Ramas por las que se ubican los nodos
    /// </summary>
    public Dictionary<INode<T>, int> BranchNode { get; private set; }

    public TableLongana(int n) : base(n)
    {
        this.BranchNode = new Dictionary<INode<T>, int>();
    }

    public void AssignCantPlayers(int players)
    {
        if (this.CantPlayer == 0)
        {
            this.FreeNode = new HashSet<INode<T>>();
            this.TableNode = new List<INode<T>>();
            this.CantPlayer = players;
            INode<T> node = this.CreateNode(players);
            this.FreeTable(node);
            this.BranchNode.Add(node, -1);
        }
    }

    protected override void Expand(INode<T> node)
    {
        //Asignar el Id de cada rama a los nodos y expandir diferene la primera vez
        if (this.PlayNode.Count == 0)
        {
            for (int i = 0; i < node.Connections.Length; i++)
            {
                UnionNode(node, CreateNode(this.DimensionToken), i);
                AssignValueConnection(node, node.Connections[i]!, 0);
                FreeTable(node.Connections[i]!);
                this.BranchNode.Add(node.Connections[i]!, i);
            }
        }
        else
        {
            base.Expand(node);
            for (int i = 0; i < node.Connections.Length; i++)
            {
                if (!this.BranchNode.ContainsKey(node.Connections[i]!))
                {
                    this.BranchNode.Add(node.Connections[i]!, this.BranchNode[node]);
                }
            }
        }
    }

    public override TableGame<T> Clone()
    {
        TableGame<T> table = new TableLongana<T>(this.DimensionToken);
        ((TableLongana<T>) table).AssignCantPlayers(this.CantPlayer);

        return AuxClone(table);
    }

    protected override void UnionNode(INode<T> left, INode<T> right, int ind)
    {
        if (this.PlayNode.Count == 0)
        {
            right.Connections[0] = left;
            left.Connections[ind] = right;
        }
        else base.UnionNode(right, left, ind);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}