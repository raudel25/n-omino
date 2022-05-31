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
        if (valueConection == -1) return true;
        for (int i = 0; i < token.CantValues; i++)
        {
            if (token.Values[i] == valueConection) return true;
        }
        return false;
    }
    public int[] AsignValues(Node node, Token token, TableGame table)
    {
        if (token.CantValues != node.Conections.Length) return null!;
        if (!table.FreeNode.Contains(node)) return null!;
        NodeDimension nodeDimension = (node as NodeDimension)!;
        if (nodeDimension == null) return null!;
        int[] values = new int[token.Values.Length];
        Array.Copy(token.Values, values, token.Values.Length);
        int ind = 0;
        int conection = 0;
        //Buscamos si hay algun valor ya predeterminado
        for (int i = 0; i < nodeDimension.ValuesConections.Length; i++)
        {
            if (nodeDimension.ValuesConections[i] != -1)
            {
                conection = nodeDimension.ValuesConections[i];
                ind = i;
                break;
            }
        }
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == conection)
            {
                //Realizamos el cambio correspondiente con el valor preasignado
                values[i] = values[ind];
                values[ind] = conection;
                break;
            }
        }
        return values;
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
            if (IqualValues(nodeGeometry, tableGeometry, circular)) return true;
        }
        return false;
    }
    public int[] AsignValues(Node node, Token token, TableGame table)
    {
        if (token.CantValues != node.Conections.Length) return null!;
        if (!table.FreeNode.Contains(node)) return null!;
        TableGeometry tableGeometry = (table as TableGeometry)!;
        NodeGeometry nodeGeometry = (node as NodeGeometry)!;
        if (nodeGeometry == null || tableGeometry == null) return null!;
        for (int i = 0; i < token.CantValues; i++)
        {
            int[] circular = CircularArray<int>.Circular(token.Values, i);
            if (IqualValues(nodeGeometry, tableGeometry, circular)) return circular;
        }
        return null!;
    }
    private bool IqualValues(NodeGeometry nodeGeometry, TableGeometry tableGeometry, int[] circular)
    {
        for (int j = 0; j < circular.Length; j++)
        {
            if (tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]] != -1 && tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]] != circular[j])
            {
                return false;
            }
        }
        return true;
    }
}