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
    public int[] AssignValues(INode node, Token token, TableGame table);
}

public class ValidPlayDimension : IValidPlay
{
    /// <summary>
    /// Criterio para comparar los valores de las fichas
    /// </summary>
    private IComparison _comparison;

    public ValidPlayDimension(IComparison comp)
    {
        this._comparison = comp;
    }

    public bool ValidPlay(INode node, Token token, TableGame table)
    {
        if (token.Values.Length != node.Connections.Length) return false;
        if (!table.FreeNode.Contains(node)) return false;
        NodeDimension? nodeDimension = (node as NodeDimension);
        if (nodeDimension == null) return false;
        int connection = nodeDimension.FirstConnection;
        int valueConnection = nodeDimension.ValuesConnections[connection];
        if (valueConnection == -1) return true;
        for (int i = 0; i < token.Values.Length; i++)
        {
            if (this._comparison.Compare(token.Values[i], valueConnection)) return true;
        }

        return false;
    }

    public int[] AssignValues(INode node, Token token, TableGame table)
    {
        if (token.Values.Length != node.Connections.Length) return new[] {-1};
        if (!table.FreeNode.Contains(node)) return new[] {-1};
        NodeDimension? nodeDimension = (node as NodeDimension);
        if (nodeDimension == null) return new[] {-1};
        int[] values = new int[token.Values.Length];
        Array.Copy(token.Values, values, token.Values.Length);
        int ind = 0;
        int connection = 0;
        //Buscamos si hay algun valor ya predeterminado
        for (int i = 0; i < nodeDimension.ValuesConnections.Length; i++)
        {
            if (nodeDimension.ValuesConnections[i] != -1)
            {
                connection = nodeDimension.ValuesConnections[i];
                ind = i;
                break;
            }
        }

        for (int i = 0; i < values.Length; i++)
        {
            if (this._comparison.Compare(values[i], connection))
            {
                //Realizamos el cambio correspondiente con el valor preasignado
                values[i] = values[ind];
                values[ind] = connection;
                break;
            }
        }

        return values;
    }
}

public class ValidPlayGeometry : IValidPlay
{
    private IComparison _comparation;

    public ValidPlayGeometry(IComparison comp)
    {
        this._comparation = comp;
    }

    public bool ValidPlay(INode node, Token token, TableGame table)
    {
        return AssignValues(node, token, table)[0] != -1;
    }

    public int[] AssignValues(INode node, Token token, TableGame table)
    {
        if (token.Values.Length != node.Connections.Length) return new[] {-1};
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
            if (tableGeometry.CoordValor[nodeGeometry.Location.Coord[j]] != -1 &&
                !this._comparation.Compare(tableGeometry.CoordValor[nodeGeometry.Location.Coord[j]], circular[j]))
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

    public int[] AssignValues(INode node, Token token, TableGame table)
    {
        return this._comodinToken.Values.ToArray();
    }
}