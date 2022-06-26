using InfoGame;
using Table;

namespace Rules;

public interface IValidPlay<T> where T : struct
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

public class ValidPlayDimension<T> : IValidPlay<T> where T : struct
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
        if (token.CantValues != node.Connections.Length) return false;
        if (!table.FreeNode.Contains(node)) return false;

        NodeDimension<T>? nodeDimension = node as NodeDimension<T>;
        if (nodeDimension == null) return false;

        int connection = nodeDimension.FirstConnection;
        if (connection == -1) return true;

        ValuesNode<T> value = table.ValuesNodeTable(nodeDimension, connection)!;

        foreach (var item in token)
        {
            if (this._comparison.Compare(item, value.Values[0])) return true;
        }

        return false;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        if (token.CantValues != node.Connections.Length) return Array.Empty<T>();
        if (!table.FreeNode.Contains(node)) return Array.Empty<T>();

        NodeDimension<T>? nodeDimension = node as NodeDimension<T>;
        if (nodeDimension == null) return Array.Empty<T>();

        T[] values = new T[token.CantValues];
        Array.Copy(token.ToArray(), values, token.CantValues);

        int ind = nodeDimension.FirstConnection;
        if (ind == -1) return values;

        ValuesNode<T> value = table.ValuesNodeTable(nodeDimension, ind)!;

        for (int i = 0; i < values.Length; i++)
        {
            if (this._comparison.Compare(values[i], value.Values[0]))
            {
                //Realizamos el cambio correspondiente con el valor preasignado
                values[i] = values[ind];
                values[ind] = value.Values[0];
                break;
            }
        }

        return values;
    }
}

public class ValidPlayGeometry<T> : IValidPlay<T> where T : struct
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
        if (token.CantValues != node.Connections.Length) return Array.Empty<T>();
        if (!table.FreeNode.Contains(node)) return Array.Empty<T>();

        TableGeometry<T>? tableGeometry = table as TableGeometry<T>;
        NodeGeometry<T>? nodeGeometry = node as NodeGeometry<T>;
        if (nodeGeometry == null || tableGeometry == null) return Array.Empty<T>();

        for (int i = 0; i < token.CantValues; i++)
        {
            T[] circular = AuxTable.CircularArray(token, i).ToArray();
            if (ValuesEquals(nodeGeometry, tableGeometry, circular)) return circular;
        }

        return Array.Empty<T>();
    }

    private bool ValuesEquals(NodeGeometry<T> nodeGeometry, TableGeometry<T> tableGeometry, T[] circular)
    {
        for (int j = 0; j < circular.Length; j++)
        {
            ValuesNode<T> valuesNode = tableGeometry.ValuesNodeTable(nodeGeometry, j)!;
            if (valuesNode.IsAssignValue)
            {
                foreach (var item in valuesNode.Values)
                {
                    if (!this._comparison.Compare(item, circular[j]))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}

public class ComodinTokenDimension<T> : IValidPlay<T> where T : struct
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
        return this._comodinToken.ToArray();
    }
}

public class ValidPlayLongana<T> : IValidPlay<T> where T : struct
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
            T aux = token[0];
            foreach (var item in token)
            {
                if (aux!.Equals(item)) return true;
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
                aux[i] = token[0];
            }

            return aux;
        }
        else if (tableLongana.BranchNode[node] == this._player) return this._valid.AssignValues(node, token, table);

        return Array.Empty<T>();
    }
}