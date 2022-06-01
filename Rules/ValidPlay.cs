using Rules;
using Table;

namespace Game;

public class ValidPlayDimension : IValidPlay
{
    public bool ValidPlay(Node node, Token token, TableGame table)
    {
        if (token.CantValues != node.Conections.Length) return false;
        if (!table.FreeNode.Contains(node)) return false;
        NodeDimension nodeDimension = (node as NodeDimension)!;
        if (nodeDimension == null) return false;
        int conection = nodeDimension.FirstConection;
        int valueConection = nodeDimension.ValuesConections[conection];
        for (int i = 0; i < token.CantValues; i++)
        {
            if (token.Values[i] == valueConection) return true;
        }
        return false;
    }
}

public class ValidPlayGeometry : IValidPlay
{
    public bool ValidPlay(Node node, Token token, TableGame table)
    {
        if (token.CantValues != node.Conections.Length) return false;
        if (!table.FreeNode.Contains(node)) return false;
        TableGeometry tableGeometry = (table as TableGeometry)!;
        NodeGeometry nodeGeometry = (node as NodeGeometry)!;
        if (nodeGeometry == null || tableGeometry == null) return false;
        for (int i = 0; i < token.CantValues; i++)
        {
            int[] circular = CircularArray<int>.Circular(token.Values, i);
            bool iquals = true;
            for (int j = 0; j < token.CantValues; j++)
            {
                if (tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]] != -1 && tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]] != circular[j])
                {
                    iquals = false;
                    break;
                }
            }
            if (iquals) return true;
        }
        return false;
    }
}