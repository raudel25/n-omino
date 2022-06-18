using InfoGame;
using Table;

namespace Rules;

public interface IValidPlay<T>
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa de juego</param>
    /// <returns>Si el criterio es valido</returns>
    public bool ValidPlay(INode<T> node, Token<T> token, TableGame<T> table);

    /// <summary>Determinar los valores para asignar al nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Valores a asignar al nodo, retorna una array cuyo primer
    /// elemento es -1 si el criterio no es valido</returns>
    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table);
}

public class ValidPlayDimension<T> : IValidPlay<T>
{
    /// <summary>
    /// Criterio para comparar los valores de las fichas
    /// </summary>
    private IComparison<T> _comparison;

    public ValidPlayDimension(IComparison<T> comp)
    {
        this._comparison = comp;
    }

    public bool ValidPlay(INode<T> node, Token<T> token, TableGame<T> table)
    {
        if (token.Values.Length != node.Connections.Length) return false;
        if (!table.FreeNode.Contains(node)) return false;

        NodeDimension<T>? nodeDimension = node as NodeDimension<T>;
        if (nodeDimension == null) return false;

        int connection = nodeDimension.FirstConnection;
        if (connection == -1) return true;
        T valueConnection = nodeDimension.ValuesConnections[connection];

        for (int i = 0; i < token.Values.Length; i++)
        {
            if (this._comparison.Compare(token.Values[i], valueConnection)) return true;
        }

        return false;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        if (token.Values.Length != node.Connections.Length) return Array.Empty<T>();
        if (!table.FreeNode.Contains(node)) return Array.Empty<T>();

        NodeDimension<T>? nodeDimension = node as NodeDimension<T>;
        if (nodeDimension == null) return Array.Empty<T>();

        T[] values = new T[token.Values.Length];
        Array.Copy(token.Values, values, token.Values.Length);
        int ind = nodeDimension.FirstConnection;
        if (ind == -1) return values;
        T connection = nodeDimension.ValuesConnections[ind];

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

public class ValidPlayGeometry<T> : IValidPlay<T>
{
    private IComparison<T> _comparison;

    public ValidPlayGeometry(IComparison<T> comp)
    {
        this._comparison = comp;
    }

    public bool ValidPlay(INode<T> node, Token<T> token, TableGame<T> table)
    {
        return AssignValues(node, token, table).Length != 0;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        if (token.Values.Length != node.Connections.Length) return Array.Empty<T>();
        if (!table.FreeNode.Contains(node)) return Array.Empty<T>();

        TableGeometry<T>? tableGeometry = table as TableGeometry<T>;
        NodeGeometry<T>? nodeGeometry = node as NodeGeometry<T>;
        if (nodeGeometry == null || tableGeometry == null) return Array.Empty<T>();

        for (int i = 0; i < token.Values.Length; i++)
        {
            T[] circular = AuxTable.CircularArray(token.Values, i);
            if (ValuesEquals(nodeGeometry, tableGeometry, circular)) return circular;
        }

        return Array.Empty<T>();
    }

    private bool ValuesEquals(NodeGeometry<T> nodeGeometry, TableGeometry<T> tableGeometry, T[] circular)
    {
        for (int j = 0; j < circular.Length; j++)
        {
            if (tableGeometry.CoordValor[nodeGeometry.Location.Coord[j]].Item2 &&
                !this._comparison.Compare(tableGeometry.CoordValor[nodeGeometry.Location.Coord[j]].Item1!,
                    circular[j]))
            {
                return false;
            }
        }

        return true;
    }
}

public class ComodinTokenDimension<T> : IValidPlay<T>
{
    private Token<T> _comodinToken;

    public ComodinTokenDimension(Token<T> token)
    {
        this._comodinToken = token;
    }

    public bool ValidPlay(INode<T> node, Token<T> token, TableGame<T> table)
    {
        return _comodinToken.Equals(token);
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        return this._comodinToken.Values.ToArray();
    }
}

public class ValidPlayLongana<T> : IValidPlay<T>
{
    private IComparison<T> _comparison;

    /// <summary>
    /// Criterior valido para comparar
    /// </summary>
    private IValidPlay<T> _valid;

    /// <summary>
    /// Indice del jugador
    /// </summary>
    private int _player;

    public ValidPlayLongana(IComparison<T> comp, IValidPlay<T> valid, int player)
    {
        this._comparison = comp;
        this._valid = valid;
        this._player = player;
    }

    public bool ValidPlay(INode<T> node, Token<T> token, TableGame<T> table)
    {
        TableLongana<T>? tableLongana = table as TableLongana<T>;
        if (tableLongana == null) return false;

        if (tableLongana.PlayNode.Count == 0)
        {
            T aux = token.Values[0];
            for (int i = 1; i < token.Values.Length; i++)
            {
                if (aux!.Equals(token.Values[i])) return true;
            }

            return false;
        }
        else if (tableLongana.BranchNode[node] == this._player) return this._valid.ValidPlay(node, token, table);

        return false;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        TableLongana<T>? tableLongana = table as TableLongana<T>;
        if (tableLongana == null) return Array.Empty<T>();

        if (tableLongana.PlayNode.Count == 0)
        {
            T[] aux = new T[node.Connections.Length];
            for (int i = 0; i < node.Connections.Length; i++)
            {
                aux[i] = token.Values[0];
            }

            return aux;
        }
        else if (tableLongana.BranchNode[node] == this._player) return this._valid.AssignValues(node, token, table);

        return Array.Empty<T>();
    }
}