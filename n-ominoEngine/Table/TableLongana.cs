namespace Table;

public class TableLongana<T> : TableDimension<T>
{
    public TableLongana(int n) : base(n)
    {
        BranchNode = new Dictionary<INode<T>, int>();
    }

    /// <summary>
    ///     Cantidad de jugadores
    /// </summary>
    public int CantPlayer { get; private set; }

    /// <summary>
    ///     Ramas por las que se ubican los nodos
    /// </summary>
    public Dictionary<INode<T>, int> BranchNode { get; }

    public void AssignCantPlayers(int players)
    {
        if (CantPlayer == 0)
        {
            FreeNode = new HashSet<INode<T>>();
            TableNode = new List<INode<T>>();
            CantPlayer = players;
            var node = CreateNode(players);
            FreeTable(node);
            BranchNode.Add(node, -1);
        }
    }

    protected override void Expand(INode<T> node)
    {
        //Asignar el Id de cada rama a los nodos y expandir diferene la primera vez
        if (PlayNode.Count == 0)
        {
            for (var i = 0; i < node.Connections.Length; i++)
            {
                UnionNode(node, CreateNode(DimensionToken), i);
                AssignValueConnection(node, node.Connections[i]!, 0);
                FreeTable(node.Connections[i]!);
                BranchNode.Add(node.Connections[i]!, i);
            }
        }
        else
        {
            base.Expand(node);
            for (var i = 0; i < node.Connections.Length; i++)
                if (!BranchNode.ContainsKey(node.Connections[i]!))
                    BranchNode.Add(node.Connections[i]!, BranchNode[node]);
        }
    }

    public override TableGame<T> Clone()
    {
        TableGame<T> table = new TableLongana<T>(DimensionToken);
        ((TableLongana<T>)table).AssignCantPlayers(CantPlayer);

        return AuxClone(table);
    }

    public override TableGame<T> Reset()
    {
        return new TableLongana<T>(DimensionToken);
    }

    protected override void UnionNode(INode<T> left, INode<T> right, int ind)
    {
        if (PlayNode.Count == 0)
        {
            right.Connections[0] = left;
            left.Connections[ind] = right;
        }
        else
        {
            base.UnionNode(right, left, ind);
        }
    }
}