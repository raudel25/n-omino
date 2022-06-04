using InfoGame;
using Table;

namespace Rules;

public class ValidPlayDimension : IValidPlay
{
    private IComparation _comparation { get; set; }
    public ValidPlayDimension(IComparation comp = null!)
    {
        if (comp == null) comp = new ClasicComparation();
        this._comparation = comp;
    }
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
            if (this._comparation.Compare(token.Values[i], valueConection)) return true;
        }
        return false;
    }
    /// <summary>Determinar los valores para asignar al nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
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
            if (this._comparation.Compare(values[i], conection))
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
    private IComparation _comparation { get; set; }
    public ValidPlayGeometry(IComparation comp = null!)
    {
        if (comp == null) comp = new ClasicComparation();
        this._comparation = comp;
    }
    public bool ValidPlay(Node node, Token token, TableGame table)
    {
        int[] aux = AsignValues(node, token, table);
        return aux != null;
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
            if (tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]] != -1 && !this._comparation.Compare(tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]], circular[j]))
            {
                return false;
            }
        }
        return true;
    }
}