using Table;

namespace Rules;

public interface IValidPlay
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Si el criterio es valido</returns>
    public bool ValidPlay(INode node, Token token, TableGame table);

    /// <summary>Determinar los valores para asignar al nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Valores a asignar al nodo, retorna una array cuyo primer
    /// elemento es -1 si el criterio no es valido</returns>
    public int[] AsignValues(INode node, Token token, TableGame table);
}

public class ValidPlayDimension : IValidPlay
{
    /// <summary>
    /// Criterio para comparar los valores de las fichas
    /// </summary>
    private IComparation _comparation;

    public ValidPlayDimension(IComparation comp)
    {
        this._comparation = comp;
    }

    public bool ValidPlay(INode node, Token token, TableGame table)
    {
        if (token.Values.Length != node.Conections.Length) return false;
        if (!table.FreeNode.Contains(node)) return false;
        NodeDimension? nodeDimension = (node as NodeDimension);
        if (nodeDimension == null) return false;
        int conection = nodeDimension.FirstConection;
        int valueConection = nodeDimension.ValuesConections[conection];
        if (valueConection == -1) return true;
        for (int i = 0; i < token.Values.Length; i++)
        {
            if (this._comparation.Compare(token.Values[i], valueConection)) return true;
        }

        return false;
    }

    public int[] AsignValues(INode node, Token token, TableGame table)
    {
        if (token.Values.Length != node.Conections.Length) return new[] {-1};
        if (!table.FreeNode.Contains(node)) return new[] {-1};
        NodeDimension? nodeDimension = (node as NodeDimension);
        if (nodeDimension == null) return new[] {-1};
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
    private IComparation _comparation;

    public ValidPlayGeometry(IComparation comp)
    {
        this._comparation = comp;
    }

    public bool ValidPlay(INode node, Token token, TableGame table)
    {
        return AsignValues(node, token, table)[0] != -1;
    }

    public int[] AsignValues(INode node, Token token, TableGame table)
    {
        if (token.Values.Length != node.Conections.Length) return new[] {-1};
        if (!table.FreeNode.Contains(node)) return new[] {-1};
        TableGeometry? tableGeometry = (table as TableGeometry);
        NodeGeometry? nodeGeometry = (node as NodeGeometry);
        if (nodeGeometry == null || tableGeometry == null) return new[] {-1};
        for (int i = 0; i < token.Values.Length; i++)
        {
            int[] circular = AuxTable.CircularArray(token.Values, i);
            if (IqualValues(nodeGeometry, tableGeometry, circular)) return circular;
        }

        return new[] {-1};
    }

    private bool IqualValues(NodeGeometry nodeGeometry, TableGeometry tableGeometry, int[] circular)
    {
        for (int j = 0; j < circular.Length; j++)
        {
            if (tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]] != -1 &&
                !this._comparation.Compare(tableGeometry.CoordValor[nodeGeometry.Ubication.Coord[j]], circular[j]))
            {
                return false;
            }
        }

        return true;
    }
}

public class ComodinTokenDimension : IValidPlay
{
    private Token _comodinToken;

    public ComodinTokenDimension(Token token)
    {
        this._comodinToken = token;
    }

    public bool ValidPlay(INode node, Token token, TableGame table)
    {
        return _comodinToken.Equals(token);
    }

    public int[] AsignValues(INode node, Token token, TableGame table)
    {
        return this._comodinToken.Values.ToArray();
    }
}